using skb_sec._Project.Core.Pool_Manager.Script;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Pick_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Expense_Effect_System
{
    public class ExpenseEffectManager : MonoBehaviour
    {
        public static ExpenseEffectManager instance;

        private Pickable _expenseItem;
        

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
        }

        public void Expense(ResourceSO resource, Vector3 startPos, Vector3 endPos)
        {
            _expenseItem = PoolManager.instance.GetItemFromPool(resource.poolItemType).GetComponent<Pickable>();

            _expenseItem.InitExpenseable(resource.poolItemType, startPos, endPos);
        }
    }
}