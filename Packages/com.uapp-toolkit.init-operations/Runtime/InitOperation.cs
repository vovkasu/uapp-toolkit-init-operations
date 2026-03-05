using System;

namespace UAppToolKit.InitOperations
{
    public class InitOperation : InitOperationBase
    {
        private Action _body;
        private bool _isEvaluated;

        public InitOperation(string operationName, Action body) : base(operationName)
        {
            _body = body;
            _isEvaluated = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_isEvaluated)
            {
                _body();
                _isEvaluated = true;
                return true;
            }
            return !_isEvaluated;
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