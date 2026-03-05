using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UAppToolKit.InitOperations
{
    public class InitOperationTask : InitOperationBase
    {
        private bool _isStarted;
        private readonly Func<Task> _taskFunc;
        private Task _task;

        public InitOperationTask(string operationName, Func<Task> taskFunc) : base(operationName)
        {
            _taskFunc = taskFunc;
            _isStarted = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_isStarted)
            {
                _task = _taskFunc();
                _isStarted = true;
                return true;
            }

            var moveNextInternal = _isStarted && !_task.IsCompleted;
            if (!moveNextInternal)
            {
                if (_task != null && _task.IsFaulted)
                {
                    Debug.LogException(_task.Exception);
                }
            }
            return moveNextInternal;
        }

        public override void ResetInternal()
        {
            _isStarted = false;
        }

        public override float Current => _isStarted && _task != null && _task.IsCompleted ? 1f : 0f;
        public override void Dispose()
        {
            if (_task != null)
            {
                _task.Dispose();
            }
        }
    }
}