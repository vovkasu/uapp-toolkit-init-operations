using System.Collections;

namespace UAppToolKit.InitOperations
{
    public class AsyncInitOperation : InitOperationBase
    {
        private IEnumerator _body;
        private bool _isCompleted;

        public AsyncInitOperation(string operationName, IEnumerator body) : base(operationName)
        {
            _body = body;
            _isCompleted = false;
        }

        protected override bool MoveNextInternal()
        {
            var moveNext = _body.MoveNext();
            _isCompleted = !moveNext;
            return moveNext;
        }

        public override void ResetInternal()
        {
            _body.Reset();
            _isCompleted = false;
        }

        public override float Current => _isCompleted ? 1f : 0f;
        public override void Dispose()
        {
            _body = null;
        }
    }
}