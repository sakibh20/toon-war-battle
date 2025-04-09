using System;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Core.Pool_Manager.Script
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private Transform poolItemParent;
        
        [Space(10)]
        [SerializeField] private List<PoolItem> poolItems;
        
        private readonly Dictionary<PoolItemType, Queue<GameObject>> _availableItems = new Dictionary<PoolItemType, Queue<GameObject>>();
        
        public static PoolManager instance;

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
        }

        private void Start()
        {
            GeneratePool();
        }

        private void GeneratePool()
        {
            foreach (var item in poolItems)
            {
                GameObject poolItem = Instantiate(item.itemPrefab, poolItemParent);
                
                Queue<GameObject> objectQueue = new Queue<GameObject>();
                objectQueue.Enqueue(poolItem);
                _availableItems.Add(item.poolItemType, objectQueue);
            }
        }

        [Button]
        public GameObject GetItemFromPool(PoolItemType type)
        {
            GameObject poolItem;
            
            if (_availableItems.TryGetValue(type, out Queue<GameObject> objectQueue))
            {
                poolItem = objectQueue.Count > 0 ? objectQueue.Dequeue() : AddNewItem(type);
            }
            
            else
            {
                CustomDebug.Log("Key Not Found");
                poolItem = AddNewItem(type);
            }
        
            return poolItem;
        }
        
        private GameObject AddNewItem(PoolItemType poolItemType)
        {
            foreach (var item in poolItems)
            {
                if (item.poolItemType == poolItemType)
                {
                    GameObject poolItem = Instantiate(item.itemPrefab, poolItemParent);
                    //CustomDebug.Log("new generated: "+poolItem.name);

                    return poolItem;
                }
            }

            return null;
        }
        
        [Button]
        public void PutBackItemInPool(PoolItemType poolItemType, GameObject poolItem)
        {
            if (_availableItems.TryGetValue(poolItemType, out Queue<GameObject> objectQueue))
            {
                poolItem.transform.SetParent(poolItemParent);
                objectQueue.Enqueue(poolItem);
            }
        }
    }

    [Serializable]
    public class PoolItem
    {
        public PoolItemType poolItemType;
        public GameObject itemPrefab;
    }

    public enum PoolItemType
    {
        TreePart,
        RockPart,
        GoldPart,
    }
}