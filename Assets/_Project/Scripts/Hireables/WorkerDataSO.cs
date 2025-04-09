using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Hireable/Create New Worker Data", fileName = "Worker Data - ")]
    public class WorkerDataSO : ScriptableObject
    {
        public ResourceSO hiringResourceSo;
        //public int hiringCost;
        
        public ResourceSO storingResourceSo;
        public UnlockableDataSO storageUnlockableDataSo;

        public WorkerAllLevelsSO workerAllLevelsSo;
        
        // public bool hired;
        // public int storedValue;
    }
}