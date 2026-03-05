using System;

namespace UAppToolKit.InitOperations
{
    public class InitCallbackOperation : InitOperationBase
    {
        private Action<Action> _body;
        private bool _isStarted;
        private bool _isCompleted;

        public InitCallbackOperation(string operationName, Action<Action> body) : base(operationName)
        {
            _body = body;
            _isStarted = false;
            _isCompleted = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_isStarted)
            {
                _isStarted = true;
                _body(Completed);
            }
            return !_isCompleted;
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