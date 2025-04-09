using System;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Task_Manager;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    public class WoodCollectTaskHandler : Task
    {
        [SerializeField, Required] private ResourceSO resourceData;

        private int _count;
        
        private void Subscribe()
        {
            resourceData.OnValueIncreased += TaskProgress;
        }

        private void UnSubscribe()
        {
            resourceData.OnValueIncreased -= TaskProgress;
        }

        private void TaskProgress()
        {
            _count += 1;

            _count = _count > taskData.countToComplete ? taskData.countToComplete : _count;
            
            OnTaskProgress(_count);
            
            if (_count >= taskData.countToComplete)
            {
                UnSubscribe();
                OnTaskComplete();
            }
        }
        

        protected override void TaskStarted()
        {
            Subscribe();
        }
    }
}
