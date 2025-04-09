using System;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Interactable/Unlockables/New Unlockable", fileName = "New Unlockable")]
    public class UnlockableDataSO : ScriptableObject
    {
        [ShowInInspector] public UnlockableData unlockableData;
        public ResourceSO requiredResourceSo;

        public event Action Unlocked;
        public event Action Damaged;
        public event Action Repaired;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(unlockableData.unlockableName))
            {
                Debug.LogError("Unlockable name must not be empty or null");
            }
        }

        [Button]
        public void FireUnlocked()
        {
            Unlocked?.Invoke();
        }
        public void FireDamaged()
        {
            Damaged?.Invoke();
        }     
        
        public void FireRepaired()
        {
            Repaired?.Invoke();
        }
    }

    [Serializable]
    public struct UnlockableData
    {
        public string unlockableName;
        public int unlockCost;
        public int alreadyInvested;
        public bool isUnlocked;
        public bool isDamaged;
    }
}
