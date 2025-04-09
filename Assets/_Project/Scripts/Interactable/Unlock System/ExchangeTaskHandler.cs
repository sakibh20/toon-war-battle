using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Interactable.Exchange;
using skb_sec._Project.Scripts.Task_Manager;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class ExchangeTaskHandler : Task
    {
        [SerializeField, Required] private ExchangeDataSO exchangeDataSo;

        private int _count;
        
        private void Subscribe()
        {
            exchangeDataSo.OnExchangedComplete += TaskProgress;
        }

        private void UnSubscribe()
        {
            exchangeDataSo.OnExchangedComplete -= TaskProgress;
        }

        private void TaskProgress()
        {
            _count += exchangeDataSo.lastExchangedValue;

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
