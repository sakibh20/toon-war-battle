using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Task_Manager.All_Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace skb_sec._Project.Scripts.Task_Manager
{
    public abstract class Task : MonoBehaviour
    {
        [Header("Task Info")]
        public TaskDataSo taskData;
        [SerializeField, Required] private GameObject vCam;

        private Action _onTaskCompleteCallback;
        private Action<int> _onTaskProgressCallback;

        protected void OnTaskComplete()
        {
            _onTaskCompleteCallback?.Invoke();

            DisableVCam();
        }

        protected void OnTaskProgress(int value)
        {
            _onTaskProgressCallback?.Invoke(value);
        }
        
        public void SetAsNextTask(Action onTaskCompleteCallback, Action<int> onTaskProgressCallback)
        {
            _onTaskCompleteCallback = onTaskCompleteCallback;
            _onTaskProgressCallback = onTaskProgressCallback;

            TaskStarted();
            
            vCam.SetActive(true);
            
            
            Invoke(nameof(DisableVCam), 4.0f);
        }

        protected abstract void TaskStarted();
        
        private void DisableVCam()
        {
            CancelInvoke(nameof(DisableVCam));

            if (vCam)
            {
                vCam.SetActive(false);
            }
        }
    }
}
