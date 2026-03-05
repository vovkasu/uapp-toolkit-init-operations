using System.Collections.Generic;

namespace UAppToolKit.InitOperations
{
    public class AsyncInitOperationWithProgress : InitOperationBase
    {
        private IEnumerator<float> _body;

        public AsyncInitOperationWithProgress(string operationName, IEnumerator<float> body) : base(operationName)
        {
            _body = body;
        }

        protected override bool MoveNextInternal() => _body.MoveNext();

        public override void ResetInternal() => _body.Reset();

        public override float Current => _body.Current;
        public override void Dispose()
        {
            _body = null;
        }
    }
}