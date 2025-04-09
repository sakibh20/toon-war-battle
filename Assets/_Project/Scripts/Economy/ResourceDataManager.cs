using System;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.DataManager;
using skb_sec._Project.Scripts.Save_System.Scripts;
using UnityEngine;

namespace skb_sec._Project.Scripts.Economy
{
    public class ResourceDataManager : DataManager.DataManager
    {
        [SerializeField] private ResourceManagerSO resourceManagerSo;

        
        private AllResourceData _allResourceData;

        protected override void Subscribe()
        {
            resourceManagerSo.StoreResource += OnResourceUpdated;
        }

        protected override void UnSubscribe()
        {
            resourceManagerSo.StoreResource -= OnResourceUpdated;
        }


        protected override void GatherResourceData()
        {
            _allResourceData = new AllResourceData();
            
            for (int i = 0; i < resourceManagerSo.allResourceSo.Count; i++)
            {
                ResourceData data = new ResourceData();
                
                data.resourceName = resourceManagerSo.allResourceSo[i].resourceName;
                data.value = resourceManagerSo.allResourceSo[i].Value;

                _allResourceData.allResourceData.Add(data);
            }
        }

        protected override void SaveData()
        {
            string data = JsonUtility.ToJson(_allResourceData);
            DataSaveManager.SaveData(data, fileName);
        }


        protected override void GetData()
        {
            var data = DataSaveManager.GetData(fileName);
            _allResourceData = JsonUtility.FromJson<AllResourceData>(data);

            DistributeData();
            
            resourceManagerSo.OnResourceLoaded();
        }

        private void DistributeData()
        {
            if (_allResourceData == null)
            {
                ClearData();
                return;
            }
            
            for (int i = 0; i < _allResourceData.allResourceData.Count; i++)
            {
                for (int j = 0; j < resourceManagerSo.allResourceSo.Count; j++)
                {
                    if (_allResourceData.allResourceData[i].resourceName ==
                        resourceManagerSo.allResourceSo[j].resourceName)
                    {
                        resourceManagerSo.allResourceSo[j].Value = _allResourceData.allResourceData[i].value;
                        break;
                    }
                }
            }
        }
        
        [Button]
        public override void ClearData()
        {
            for (int i = 0; i < resourceManagerSo.allResourceSo.Count; i++)
            {
                resourceManagerSo.allResourceSo[i].Value = 0;
            }
            
            DataSaveManager.DeleteData(fileName);
        }
    }
    
    [Serializable]
    public class AllResourceData
    {
        public List<ResourceData> allResourceData = new List<ResourceData>();
    }
    

    [Serializable]
    public class ResourceData
    {
        public string resourceName;
        public int value;
    }
}
