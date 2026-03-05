using System;
using System.Collections.Generic;

namespace UAppToolKit.InitOperations
{
    public class LazyInitOperationWithProgress : InitOperationBase
    {
        private Func<IEnumerator<float>> _lazyBody;
        private IEnumerator<float> _body;
        private bool _bodyCalculated;

        public LazyInitOperationWithProgress(string operationName, Func<IEnumerator<float>> lazyBody) : base(operationName)
        {
            _lazyBody = lazyBody;
            _body = null;
            _bodyCalculated = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_bodyCalculated)
            {
                _bodyCalculated = true;
                _body = _lazyBody();
            }

            return _body.MoveNext();
        }

        public override void ResetInternal()
        {
            _bodyCalculated = false;
            _body = null;
        }

        public override float Current => _body.Current;
        public override void Dispose()
        {
            _lazyBody = null;
            _body = null;
        }
    }
}