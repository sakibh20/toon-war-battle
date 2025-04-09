using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.DataManager;
using skb_sec._Project.Scripts.Save_System.Scripts;
using skb_sec._Project.Scripts.Task_Manager;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableDatManager : DataManager.DataManager
    {
        [SerializeField] private UnlockableManagerSO unlockableManagerSo;
        
        private AllUnlockableData _allUnlockableData;

        protected override void Subscribe()
        {
            unlockableManagerSo.UnlockableUpdated += OnResourceUpdated;
        }

        protected override void UnSubscribe()
        {
            unlockableManagerSo.UnlockableUpdated -= OnResourceUpdated;
        }
        
        
        protected override void GatherResourceData()
        {
            _allUnlockableData = new AllUnlockableData();

            for (int i = 0; i < unlockableManagerSo.allUnlockableSo.Count; i++)
            {
                _allUnlockableData.allResourceData.Add(unlockableManagerSo.allUnlockableSo[i].unlockableData);
            }
        }

        protected override void SaveData()
        {
            string data = JsonUtility.ToJson(_allUnlockableData);
            DataSaveManager.SaveData(data, fileName);
        }


        protected override void GetData()
        {
            var data = DataSaveManager.GetData(fileName);
            _allUnlockableData = JsonUtility.FromJson<AllUnlockableData>(data);

            DistributeData();
            
            unlockableManagerSo.OnUnlockableLoaded();
        }

        private void DistributeData()
        {
            if (_allUnlockableData == null)
            {
                ClearData();
                return;
            }

            for (int i = 0; i < _allUnlockableData.allResourceData.Count; i++)
            {
                for (int j = 0; j < unlockableManagerSo.allUnlockableSo.Count; j++)
                {
                    if (_allUnlockableData.allResourceData[i].unlockableName ==
                        unlockableManagerSo.allUnlockableSo[j].unlockableData.unlockableName)
                    {
                        unlockableManagerSo.allUnlockableSo[j].unlockableData = _allUnlockableData.allResourceData[i];
                        break;
                    }
                }
            }
        }

        [Button]
        public override void ClearData()
        {
            for (int i = 0; i < unlockableManagerSo.allUnlockableSo.Count; i++)
            {
                unlockableManagerSo.allUnlockableSo[i].unlockableData.alreadyInvested = 0;
                unlockableManagerSo.allUnlockableSo[i].unlockableData.isUnlocked = false;
            }
            
            DataSaveManager.DeleteData(fileName);
        }
    }
    
    [Serializable]
    public class AllUnlockableData
    {
        public List<UnlockableData> allResourceData = new List<UnlockableData>();
    }
}
