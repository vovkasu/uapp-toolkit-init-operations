using System.Collections.Generic;
using System.Linq;

namespace UAppToolKit.InitOperations
{
    public class ParallelInitOperations : InitOperationBase
    {
        private readonly List<InitOperationBase> _operations;
        private List<InitOperationBase> _activeOperations;

        public ParallelInitOperations(string operationName, params InitOperationBase[] operations) : base(operationName)
        {
            _operations = operations.ToList();
            _activeOperations = _operations.ToList();
        }

        protected override bool MoveNextInternal()
        {
            for (var i = _activeOperations.Count - 1; i >= 0; i--)
            {
                if (!_activeOperations[i].MoveNext())
                {
                    _activeOperations.RemoveAt(i);
                }
            }

            return _activeOperations.Count > 0;
        }

        public override void ResetInternal()
        {
            foreach (var operation in _operations)
            {
                operation.ResetInternal();
            }
            _activeOperations = _operations.ToList();
        }

        public override float Current
        {
            get
            {
                if (_operations.Count == 0) return 1f;
                return _operations.Average(op => op.Current);
            }
        }

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
            _activeOperations.Add(operation);
        }
    }
}