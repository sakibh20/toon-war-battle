using System;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Attack_System;
using skb_sec._Project.Scripts.Interactable.Damageable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace skb_sec._Project.Scripts.Barrack.Soldier
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Soldier/SoldierManager", fileName = "SoldierManagerSo-")]
    public class SoldierManagerSO : ScriptableObject
    {
        [ShowInInspector] private List<Soldier> _allSoldier = new List<Soldier>();

        public int SoldierCount => _allSoldier.Count;

        public event Action OnSoldierListUpdated;

        private DamageableManagerSO _damageableManagerSo;
        [SerializeField, Required] private AttackDataManagerSO attackDataManagerSo;

        public void Enlist(Soldier soldier)
        {
            //CustomDebug.Log("Enlist Debug");
            if (!_allSoldier.Contains(soldier))
            {
                _allSoldier.Add(soldier);
                FireSoldierListUpdated();
            }
        }

        public void DeList(Soldier soldier)
        {
            if (_allSoldier.Contains(soldier))
            {
                _allSoldier.Remove(soldier);
                FireSoldierListUpdated();
            }
        }

        public void Attack(DamageableManagerSO damageableManagerSo)
        {
            _damageableManagerSo = damageableManagerSo;

            Subscribe();
            
            if(_damageableManagerSo.allDamageableByEnemy.Count <= 0) return;
            
            for (int i = 0; i < _allSoldier.Count; i++)
            {
                int index = Random.Range(0, _damageableManagerSo.allDamageableByEnemy.Count);
                _allSoldier[i].Attack(_damageableManagerSo.allDamageableByEnemy[index]);
                
                _damageableManagerSo.allDamageableByEnemy[index].EnlistInAttackingSoldierList(_allSoldier[i]);
            }
        }

        private void Subscribe()
        {
            UnSubscribe();

            _damageableManagerSo.OnDamageableRemoved += ReassignSoldier;
            _damageableManagerSo.OnBaseClear += OnBaseClear;
        }

        private void UnSubscribe()
        {
            _damageableManagerSo.OnDamageableRemoved -= ReassignSoldier;
            _damageableManagerSo.OnBaseClear -= OnBaseClear;
        }

        private void OnBaseClear()
        {
            for (int i = 0; i < _allSoldier.Count; i++)
            {
                _allSoldier[i].MoveBackToInitialPosition();
            }
        }
        
        public void OnPlayerComplete()
        {
            attackDataManagerSo.FireOnPlayerAttackComplete();
        }

        public void OnPlayerUnderAttackComplete()
        {
            attackDataManagerSo.FirePlayerUnderAttackComplete();
        }

        private void ReassignSoldier()
        {
            if(_damageableManagerSo.allDamageableByEnemy.Count <= 0) return;
            
            for (int i = 0; i < _allSoldier.Count; i++)
            {
                if (_allSoldier[i].CurrentDamageable() == null)
                {
                    int index = Random.Range(0, _damageableManagerSo.allDamageableByEnemy.Count);
                    _allSoldier[i].Attack(_damageableManagerSo.allDamageableByEnemy[index]);
                
                    _damageableManagerSo.allDamageableByEnemy[index].EnlistInAttackingSoldierList(_allSoldier[i]);
                }
            }
        }

        public void ClearList()
        {
            _allSoldier.Clear();
        }

        private void FireSoldierListUpdated()
        {
            OnSoldierListUpdated?.Invoke();
        }
    }
}
