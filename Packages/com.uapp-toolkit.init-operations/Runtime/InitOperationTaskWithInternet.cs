using System;
using System.Threading.Tasks;
using UAppToolKit.InternetChecker;
using UnityEngine;

namespace UAppToolKit.InitOperations
{
    public class InitOperationTaskWithInternet : InitOperationBase
    {
        private bool _isStarted;
        private bool _internetChecked;
        private readonly Func<Task> _taskFunc;
        private Task _task;
        private readonly InternetCheckerService _internetCheckerService;

        public InitOperationTaskWithInternet(string operationName, InternetCheckerService internetCheckerService,
            Func<Task> taskFunc) : base(operationName)
        {
            _internetCheckerService = internetCheckerService;
            _taskFunc = taskFunc;
            _isStarted = false;
            _internetChecked = false;
        }

        protected override bool MoveNextInternal()
        {
            if (!_isStarted)
            {
                _internetCheckerService.ExecuteWithInternet(WaitInternetEndEvaluate);
                _isStarted = true;
                return true;
            }

            var moveNextInternal = _isStarted && (!_internetChecked || !_task.IsCompleted);
            if (!moveNextInternal)
            {
                if (_task != null && _task.IsFaulted)
                {
                    Debug.LogException(_task.Exception);
                }
            }
            return moveNextInternal;

            void WaitInternetEndEvaluate()
            {
                _internetChecked = true;
                _task = _taskFunc();
            }
        }

        public override void ResetInternal()
        {
            _isStarted = false;
            _internetChecked = false;
        }

        public override float Current => _isStarted && _internetChecked && _task != null && _task.IsCompleted ? 1f : 0f;
        public override void Dispose()
        {
            if (_task != null)
            {
                _task.Dispose();
            }
        }
    }
}