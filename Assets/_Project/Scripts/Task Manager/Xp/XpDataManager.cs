using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Dialogue_System;
using skb_sec._Project.Scripts.Save_System.Scripts;
using UnityEngine;

namespace skb_sec._Project.Scripts.Task_Manager.Xp
{
    public class XpDataManager : DataManager.DataManager
    {
        [SerializeField] private XpDataManagerSO xpDataManagerSo;
        [SerializeField] private TaskDataManagerSO taskDataManagerSo;
        [SerializeField] private DialogueDataSO dialogueDataSo;

        private XpData _xpData;

        protected override void Subscribe()
        {
            xpDataManagerSo.OnLevelUpdated += OnResourceUpdated;
            taskDataManagerSo.OnTaskCompleted += OnResourceUpdated;
            taskDataManagerSo.OnShowNextTask += OnResourceUpdated;
            dialogueDataSo.OnDialogueComplete += OnResourceUpdated;
        }

        protected override void UnSubscribe()
        {
            xpDataManagerSo.OnLevelUpdated -= OnResourceUpdated;
            taskDataManagerSo.OnTaskCompleted -= OnResourceUpdated;
            taskDataManagerSo.OnShowNextTask -= OnResourceUpdated;
            dialogueDataSo.OnDialogueComplete -= OnResourceUpdated;
        }

        protected override void GatherResourceData()
        {
            _xpData = new XpData();
            
            _xpData.nextTaskIndex = taskDataManagerSo.nextTaskIndex; 
            _xpData.dialogueShowed = dialogueDataSo.showed;
            
            _xpData.currentLevel = xpDataManagerSo.currentLevel;
            _xpData.currentXpCount = xpDataManagerSo.currentXpCount;
        }

        protected override void SaveData()
        {
            string data = JsonUtility.ToJson(_xpData);
            DataSaveManager.SaveData(data, fileName);
        }
        
        protected override void GetData()
        {
            var data = DataSaveManager.GetData(fileName);
            _xpData = JsonUtility.FromJson<XpData>(data);

            DistributeData();
            
            xpDataManagerSo.FireOnLevelDataLoaded();
        }
        
        private void DistributeData()
        {
            if (_xpData == null)
            {
                ClearData();
                return;
            }
            
            xpDataManagerSo.currentLevel = _xpData.currentLevel;
            xpDataManagerSo.currentXpCount = _xpData.currentXpCount;

            taskDataManagerSo.nextTaskIndex = _xpData.nextTaskIndex;
            
            dialogueDataSo.showed = _xpData.dialogueShowed;
        }

        [Button]
        public override void ClearData()
        {
            xpDataManagerSo.currentLevel = 0;
            xpDataManagerSo.currentXpCount = 0;

            taskDataManagerSo.nextTaskIndex = 0;

            dialogueDataSo.showed = false;
            
            DataSaveManager.DeleteData(fileName);
        }
    }

    public class XpData
    {
        public int currentLevel;
        public int currentXpCount;

        public int nextTaskIndex;

        public bool dialogueShowed;
    }
}
