using UnityEngine;

namespace skb_sec._Project.Core.Pool_Manager.Script
{
    public interface IPoolItem
    {
        public void InitPickable(PoolItemType poolItemType, Transform targetTransform);
        public void InitExpenseable(PoolItemType poolItemType, Vector3 startPos, Vector3 targetPos);
        public void PutBack();
    }
}
