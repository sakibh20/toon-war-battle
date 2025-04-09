using System;
using System.Collections.Generic;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Hireable/Create Worker Data Manager", fileName = "Worker Data Manager")]
    public class WorkerDataManagerSO : ScriptableObject
    {
        public List<WorkerAllLevelsSO> allWorkerData = new List<WorkerAllLevelsSO>();

        public bool dataLoaded;
        
        public event Action WorkerDataUpdated;
        public event Action WorkerDataLoaded;

        public virtual void FireWorkerDataUpdated()
        {
            WorkerDataUpdated?.Invoke();
        }

        public virtual void FireWorkerDataLoaded()
        {
            dataLoaded = true;
            WorkerDataLoaded?.Invoke();
        }
    }
}