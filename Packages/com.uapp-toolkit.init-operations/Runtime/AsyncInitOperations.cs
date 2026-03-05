using System.Collections.Generic;
using System.Linq;

namespace UAppToolKit.InitOperations
{
    public class AsyncInitOperations : InitOperationBase
    {
        private int _operationIndex;
        private readonly List<InitOperationBase> _operations;

        public AsyncInitOperations(string operationName, params InitOperationBase[] operations) : base(operationName)
        {
            _operationIndex = 0;
            _operations = operations.ToList();
        }

        protected override bool MoveNextInternal()
        {
            var moveNext = _operations[_operationIndex].MoveNext();
            if (!moveNext)
            {
                if (_operationIndex < _operations.Count - 1)
                {
                    _operationIndex++;
                    _operations[_operationIndex].MoveNext();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public override void ResetInternal()
        {
            foreach (var operation in _operations)
            {
                operation.ResetInternal();
            }

            _operationIndex = 0;
        }

        public override float Current => (_operationIndex + _operations[_operationIndex].Current) / _operations.Count;
        public override void Dispose()
        {
            foreach (var operation in _operations)
            {
                operation.Dispose();
            }
        }

        public void Add(InitOperationBase operation)
        {
            _operations.Add(operation);
        }
    }
}