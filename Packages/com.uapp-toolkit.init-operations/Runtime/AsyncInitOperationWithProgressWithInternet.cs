using System.Collections.Generic;
using UAppToolKit.InternetChecker;

namespace UAppToolKit.InitOperations
{
    public class AsyncInitOperationWithProgressWithInternet : InitOperationBase
    {
        private IEnumerator<float> _body;
        private readonly InternetCheckerService _internetCheckerService;
        private bool _internetChecking;
        private bool _startedBody;

        public AsyncInitOperationWithProgressWithInternet(string operationName,
            InternetCheckerService internetCheckerService, IEnumerator<float> body) : base(operationName)
        {
            _internetCheckerService = internetCheckerService;
            _body = body;
            _internetChecking = false;
            _startedBody = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_internetChecking)
            {
                _internetChecking = true;
                _internetCheckerService.ExecuteWithInternet(WaitInternetEndEvaluate);
                return true;
            }

            if (!_startedBody)
            {
                return true;
            }

            return _body.MoveNext();

            void WaitInternetEndEvaluate()
            {
                _startedBody = true;
                _body.MoveNext();
            }
        }

        public override void ResetInternal() => _body.Reset();

        public override float Current
        {
            get
            {
                if (_internetChecking && !_startedBody)
                {
                    return 0f;
                }
                return _body.Current;
            }
        }

        public override void Dispose()
        {
            _body = null;
        }
    }
}