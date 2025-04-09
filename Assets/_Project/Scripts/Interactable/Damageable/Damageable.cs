using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Damageable
{
    public abstract class Damageable : MonoBehaviour
    {
        [SerializeField, Required] private DamageableManagerSO damageableManagerSo;

        [SerializeField, Required] private int health;
        
        [Space]
        [SerializeField, Required] private GameObject damagedObject;
        [SerializeField, Required] private GameObject intactObject;

        
        [SerializeField] private bool enemyStructure;
        
        [Space] [SerializeField, Required, HideIf("enemyStructure")] private UnlockableManagerSO unlockableManagerSo;
        [SerializeField, Required, HideIf("enemyStructure")] private UnlockableDataSO unlockableData;
        
        [ShowInInspector, ReadOnly] private readonly List<Soldier> _allAttackingSoldier = new List<Soldier>();

        private bool _damaged;
        private int _currentHealth;

        private void Awake()
        {
            if (!enemyStructure)
            {
                Subscribe();
            }
        }

        private void Start()
        {
            _currentHealth = health;

            EnlistAsDamageAble();
        }

        private void EnlistAsDamageAble()
        {
            if (enemyStructure)
            {
                damageableManagerSo.Enlist(this);;
            }
            else
            {
                if (!unlockableData.unlockableData.isDamaged)
                {
                    damageableManagerSo.Enlist(this);
                }
            }
        }

        private void OnDisable()
        {
            if (!enemyStructure)
            {
                UnSubscribe();
            }
            damageableManagerSo.ClearList();
        }
        

        private void Subscribe()
        {
            unlockableManagerSo.UnlockableLoaded += ManageInitialAppearance;
            //unlockableData.Damaged += ManageInitialAppearance;
        }

        private void UnSubscribe()
        {
            unlockableManagerSo.UnlockableLoaded -= ManageInitialAppearance;
            //unlockableData.Damaged -= ManageInitialAppearance;
        }

        private void ManageInitialAppearance()
        {
            if (enemyStructure)
            {
                ShowDamagedAppearance();
            }
            else
            {
                if (unlockableData.unlockableData.isDamaged)
                {
                    ShowDamagedAppearance();
                }
            }
        }

        [Button]
        public void TakeDamage(int damage)
        {
            if(_damaged) return;
            
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _damaged = true;
                OnDamaged();
            }
        }

        private void OnDamaged()
        {
            HandleOnDamaged();
            
            ShowDamagedAppearance();
            
            ClearDamageableForSoldier();
            damageableManagerSo.DeList(this);

            if (!enemyStructure)
            {
                unlockableManagerSo.OnUpdateUnlockable();
                unlockableData.unlockableData.isDamaged = true;
                unlockableData.FireDamaged();
            }
        }

        private void ShowDamagedAppearance()
        {
            damagedObject.SetActive(true);
            intactObject.SetActive(false);
        }
        
        private void ShowRepairedAppearance()
        {
            damagedObject.SetActive(false);
            intactObject.SetActive(true);
        }


        private void ClearDamageableForSoldier()
        {
            foreach (Soldier soldier in _allAttackingSoldier)
            {
                soldier.ClearDamageable();
            }
        }

        [Button]
        public void OnRepaired()
        {
            EnlistAsDamageAble();
            ShowRepairedAppearance();
            
            _currentHealth = health;

            unlockableData.unlockableData.isDamaged = false;
            unlockableData.FireRepaired();

            if (!enemyStructure)
            {
                unlockableManagerSo.OnUpdateUnlockable();
            }
        }
        
        public void EnlistInAttackingSoldierList(Soldier soldier)
        {
            if (!_allAttackingSoldier.Contains(soldier))
            {
                _allAttackingSoldier.Add(soldier);
            }
        }
        
        public void DeListInAttackingSoldierList(Soldier soldier)
        {
            if (_allAttackingSoldier.Contains(soldier))
            {
                _allAttackingSoldier.Remove(soldier);
            }
        }

        protected abstract void HandleOnDamaged();
    }
}
