using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Interactable;
using UnityEngine;

namespace skb_sec._Project.Scripts.Watch_Tower
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Enemy/In Range Enemy List", fileName = "InRangeEnemyListSO")]
    public class InRangeEnemyListSO : ScriptableObject
    {
        [ShowInInspector] protected List<IEnemy> allEnemyInRange = new List<IEnemy>();

        public event Action ScanEnemy;
        public event Action ListUpdated;
        
        public int AllEnemyCountInRange => allEnemyInRange.Count;

        public Transform GetFirstEnemy()
        {
            if (allEnemyInRange.Count > 0)
            {
                return allEnemyInRange[0].GetTransform();
            }
            return null;
        }

        public void AddEnemyInRangeList(IEnemy enemy)
        {
            if (!allEnemyInRange.Contains(enemy))
            {
                allEnemyInRange.Add(enemy);
            }
        }
        
        public void RemoveEnemyInRangeList(IEnemy enemy)
        {
            if (allEnemyInRange.Contains(enemy))
            {
                allEnemyInRange.Remove(enemy);

                FireListUpdated();
            }
        }
        
        public void ClearList()
        {
            allEnemyInRange.Clear();
        }
        
        private void FireListUpdated()
        {
            ListUpdated?.Invoke();
        }
    }
}
