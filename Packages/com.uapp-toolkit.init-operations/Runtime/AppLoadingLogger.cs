using System;
using System.Collections.Generic;
using UnityEngine;

namespace UAppToolKit.InitOperations
{
    public static class AppLoadingLogger
    {
        public static event Action<string> OnStart;
        public static event Action<string, float> OnCompleted;

        public static bool IsEnable;
        private static Dictionary<string, DateTime> _startedAt = new();

        public static void Started(string operationName)
        {
            if(!IsEnable) 
                return;

            Debug.Log($"[Start operation] {operationName}");
            _startedAt.Add(operationName, DateTime.Now);
            OnStart?.Invoke(operationName);
        }

        public static void Completed(string operationName)
        {
            if (!IsEnable)
                return;

            var duration = -1f;
            if (_startedAt.TryGetValue(operationName, out var startedAt))
            {
                duration = (float)(DateTime.Now - startedAt).TotalSeconds;
            }
            Debug.Log($"[End operation] {operationName} duration:{duration}");
            OnCompleted?.Invoke(operationName, duration);
        }
    }
}