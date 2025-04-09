using System;
using System.Collections.Generic;
using UnityEngine;

namespace skb_sec._Project.Scripts.Task_Manager.Xp
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Task Manager/Create XP Data Manager", fileName = "XP Data ManagerSO"), Serializable]
    public class XpDataManagerSO : ScriptableObject
    {
        public int currentXpCount;
        
        public int currentLevel;

        public List<Level> allLevels;

        public event Action OnLevelDataLoaded;
        public event Action OnLevelUpdated;
        public event Action OnLevelProgression;

        // public event Action OnDataUpdated;
        //
        // public void FireOnDataUpdated()
        // {
        //     OnDataUpdated?.Invoke();
        // }

        public void FireOnLevelDataLoaded()
        {
            OnLevelDataLoaded?.Invoke();
        }

        public void FireOnLevelUpdated()
        {
            OnLevelUpdated?.Invoke();
        }
        
        public void FireOnLevelProgression()
        {
            OnLevelUpdated?.Invoke();
        }
    }
    
    [Serializable]
    public class Level
    {
        public int requiredXpCount;
    }
}
