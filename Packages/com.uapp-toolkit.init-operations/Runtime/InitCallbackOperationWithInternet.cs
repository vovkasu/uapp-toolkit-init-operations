using System;
using UAppToolKit.InternetChecker;

namespace UAppToolKit.InitOperations
{
    public class InitCallbackOperationWithInternet : InitOperationBase
    {
        private Action<Action> _body;
        private bool _isStarted;
        private bool _isCompleted;
        private readonly InternetCheckerService _internetCheckerService;

        public InitCallbackOperationWithInternet(string operationName, InternetCheckerService internetCheckerService,
            Action<Action> body) : base(operationName)
        {
            _body = body;
            _internetCheckerService = internetCheckerService;
            _isStarted = false;
            _isCompleted = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_isStarted)
            {
                _isStarted = true;
                _internetCheckerService.ExecuteWithInternet(WaitInternetEndEvaluate);
            }
            return !_isCompleted;

            void WaitInternetEndEvaluate()
            {
                _body(Completed);
            }
        }

        private void Completed()
        {
            _isCompleted = true;
        }

        public override void ResetInternal()
        {
            _isStarted = false;
            _isCompleted = false;
        }

        public override float Current => _isCompleted ? 1f : 0f;
        public override void Dispose()
        {
            _body = null;
        }
    }
}