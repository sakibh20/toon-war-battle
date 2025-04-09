using System;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;

namespace skb_sec._Project.Scripts.Storage
{
    //[CreateAssetMenu(menuName = "SKB Custom SO/Storage System/Create New Storable Item", fileName = "Storable Item-")]
    [Serializable]
    public class StorableItem
    {
        public ResourceSO resource;
        public int storedValue;
        

        public void WithdrawOne(ResourceManagerSO resourceManagerSo)
        {
            if(storedValue <=0) return;

            storedValue -= 1;
            resourceManagerSo.IncreaseResource(1, resource, false);
        }
        
        public void StoreValueOne()
        {
            storedValue += 1;
        }
    }
}
