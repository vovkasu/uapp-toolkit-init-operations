using System;
using UAppToolKit.InternetChecker;

namespace UAppToolKit.InitOperations
{
    public class InitOperationWithInternet : InitOperationBase
    {
        private Action _body;
        private bool _isEvaluated;
        private readonly InternetCheckerService _internetCheckerService;
        private bool _started;

        public InitOperationWithInternet(string operationName, InternetCheckerService internetCheckerService, Action body) : base(operationName)
        {
            _internetCheckerService = internetCheckerService;
            _body = body;
            _started = false;
            _isEvaluated = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_started)
            {
                _started = true;
                _internetCheckerService.ExecuteWithInternet(WaitInternetEndEvaluate);
                return true;
            }
            return !_isEvaluated;

            void WaitInternetEndEvaluate()
            {
                _body();
                _isEvaluated = true;
            }
        }

        public override void ResetInternal()
        {
            _isEvaluated = false;
        }

        public override float Current => _isEvaluated ? 1f : 0f;
        public override void Dispose()
        {
            _body = null;
        }
    }
}