using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Save_System.Scripts;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    public class HireablesDataManager : DataManager.DataManager
    {
        [SerializeField] private WorkerDataManagerSO workerDataManagerSo;

        private AllWorkerData _allWorkerData;

        protected override void Subscribe()
        {
            workerDataManagerSo.dataLoaded = false;
            workerDataManagerSo.WorkerDataUpdated += OnResourceUpdated;
        }

        protected override void UnSubscribe()
        {
            workerDataManagerSo.WorkerDataUpdated += OnResourceUpdated;
        }
        
        protected override void GetData()
        {
            var data = DataSaveManager.GetData(fileName);
            _allWorkerData = JsonUtility.FromJson<AllWorkerData>(data);

            DistributeData();
            
            workerDataManagerSo.FireWorkerDataLoaded();
        }
        
        private void DistributeData()
        {
            if (_allWorkerData == null)
            {
                ClearData();
                return;
            }
            
            for (int i = 0; i < _allWorkerData.allWorkerDataList.Count; i++)
            {
                for (int j = 0; j < workerDataManagerSo.allWorkerData.Count; j++)
                {
                    if (_allWorkerData.allWorkerDataList[i].name ==
                        workerDataManagerSo.allWorkerData[j].title)
                    {
                        workerDataManagerSo.allWorkerData[j].currentLevel = _allWorkerData.allWorkerDataList[i].currentLevel;
                        workerDataManagerSo.allWorkerData[i].hired = _allWorkerData.allWorkerDataList[i].hired;
                        workerDataManagerSo.allWorkerData[i].storedValue = _allWorkerData.allWorkerDataList[i].storedValue;
                        break;
                    }
                }
            }
        }


        protected override void GatherResourceData()
        {
            _allWorkerData = new AllWorkerData();
            
            for (int i = 0; i < workerDataManagerSo.allWorkerData.Count; i++)
            {
                WorkerData data = new WorkerData();
                
                data.name = workerDataManagerSo.allWorkerData[i].title;
                data.currentLevel = workerDataManagerSo.allWorkerData[i].currentLevel;

                data.hired = workerDataManagerSo.allWorkerData[i].hired;
                data.storedValue = workerDataManagerSo.allWorkerData[i].storedValue;

                _allWorkerData.allWorkerDataList.Add(data);
            }
        }

        protected override void SaveData()
        {
            string data = JsonUtility.ToJson(_allWorkerData);
            DataSaveManager.SaveData(data, fileName);
        }

        [Button]
        public override void ClearData()
        {
            for (int i = 0; i < workerDataManagerSo.allWorkerData.Count; i++)
            {
                workerDataManagerSo.allWorkerData[i].currentLevel = 0;
                workerDataManagerSo.allWorkerData[i].hired = false;
                workerDataManagerSo.allWorkerData[i].storedValue = 0;
            }
            
            DataSaveManager.DeleteData(fileName);
        }
    }

    [Serializable]
    public class AllWorkerData
    {
        public List<WorkerData> allWorkerDataList = new List<WorkerData>();
    }

    [Serializable]
    public class WorkerData
    {
        public string name;
        public int currentLevel;
        public bool hired;
        public int storedValue;
    }
}
