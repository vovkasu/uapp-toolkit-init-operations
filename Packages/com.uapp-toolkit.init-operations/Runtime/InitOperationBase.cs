using System.Collections;
using System.Collections.Generic;

namespace UAppToolKit.InitOperations
{
    public abstract class InitOperationBase : IEnumerator<float>
    {
        private bool _isStarted;
        private bool _isCompleted;
        private float _startTime;

        public string OperationName { get; private set; }
        protected InitOperationBase(string operationName)
        {
            OperationName = operationName;
            _isStarted = false;
            _isCompleted = false;
        }

        protected abstract bool MoveNextInternal();
        public bool MoveNext()
        {
            if (!_isStarted)
            {
                _isStarted = true;
                AppLoadingLogger.Started(OperationName);
            }

            var moveNext = MoveNextInternal();
            if (!moveNext && !_isCompleted)
            {
                _isCompleted = true;
                AppLoadingLogger.Completed(OperationName);
            }

            return moveNext;
        }

        public void Reset()
        {
            _isStarted = false;
            _isCompleted = false;
            ResetInternal();
        }

        public abstract void ResetInternal();

        public abstract float Current { get; }

        object IEnumerator.Current => Current;

        public abstract void Dispose();
    }
}