using System;
using skb_sec._Project.Scripts.Storage;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    public class HireableUiManager : MonoBehaviour
    {
        [SerializeField] private WorkerDataSO workerDataSo;
        [SerializeField] private ResourceCountWorldInfo resourceCountWorldInfo;
        
        private void OnEnable()
        {
            resourceCountWorldInfo.InitHireableInfo(workerDataSo);
        }

        public void UpdateInfo()
        {
            resourceCountWorldInfo.UpdateInfo();
        }
    }
}
