using System;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Interactable/Unlockables/Unlockable Manager", fileName = "Unlockable Manager SO")]
    public class UnlockableManagerSO : ScriptableObject
    {
        public List<UnlockableDataSO> allUnlockableSo;

        public event Action UnlockableUpdated;
        public event Action UnlockableLoaded;
        
        
        public void OnUpdateUnlockable()
        {
            UnlockableUpdated?.Invoke();
        }

        public void OnUnlockableLoaded()
        {
            UnlockableLoaded?.Invoke();
        }
    }
}
