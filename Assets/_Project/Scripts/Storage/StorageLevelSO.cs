using System;
using System.Collections.Generic;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;

namespace skb_sec._Project.Scripts.Storage
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Storage System/Create New Storage Level", fileName = "New Storage Level 1")]
    public class StorageLevelSO : ScriptableObject
    {
        public List<StorableItem> allIStorable = new List<StorableItem>();
        public int upgradeCost;
        public int maxCapacity;
    }
}
