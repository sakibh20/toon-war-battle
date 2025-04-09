using System;
using skb_sec._Project.Scripts.Storage;
using UnityEngine;

namespace skb_sec._Project.Scripts.Upgrade
{
    public abstract class UpgradableSO : ScriptableObject
    {
        private int _currentLevel;
        public string title;
        public Sprite sprite;

        public int CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                FireOnLevelUpgraded();
            }
        }

        public event Action OnLevelUpgraded;
        
        public abstract void Distribute(AllStorageLevelData allUpgradableData);
        public abstract void ClearData();

        private void FireOnLevelUpgraded()
        {
            OnLevelUpgraded?.Invoke();
        }
    }
}
