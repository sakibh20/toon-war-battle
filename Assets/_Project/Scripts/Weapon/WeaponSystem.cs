using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Attack_System;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Storage;
using skb_sec._Project.Scripts.Watch_Tower;
using UnityEngine;

namespace skb_sec._Project.Scripts.Weapon
{
    public abstract class WeaponSystem : MonoBehaviour
    {
        [SerializeField, Required] private UnlockableManagerSO unlockableManagerSo;
        [SerializeField, Required] private UnlockableDataSO unlockableDataSo;
        
        [SerializeField, Required] private AttackDataManagerSO attackDataManagerSo;
        
        [SerializeField, Required] private StorageUpgradeLevelsSO allLevelsData;
        [SerializeField, Required] private InRangeEnemyListSO enemyListSo;
        [SerializeField, Required] private WeaponSO weaponSo;
        
        [SerializeField, Required] private Transform arrowBase;
        
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float scanRadius;

        [SerializeField] private int maxInteractable;


        private bool _fireAble;
        
        private void Awake()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            //unlockableManagerSo.UnlockableLoaded += OnUnlockableLoaded;

            allLevelsData.OnLevelUpgraded += OnLevelUpgraded;

            unlockableDataSo.Damaged += OnDamaged;
            unlockableDataSo.Repaired += OnRepaired;

            attackDataManagerSo.OnPlayerUnderAttack += StartScan;
            attackDataManagerSo.OnPlayerUnderAttackComplete += StopScan;
        }

        private void UnSubscribe()
        {
            //unlockableManagerSo.UnlockableLoaded -= OnUnlockableLoaded;
            
            allLevelsData.OnLevelUpgraded -= OnLevelUpgraded;
            
            unlockableDataSo.Damaged += OnDamaged;
            unlockableDataSo.Repaired += OnRepaired;
            
            attackDataManagerSo.OnPlayerUnderAttack -= StartScan;
            attackDataManagerSo.OnPlayerUnderAttackComplete -= StopScan;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        
        // private void Start()
        // {
        //     OnUnlockableLoaded();
        // }
        //
        // private void OnUnlockableLoaded()
        // {
        //     if (unlockableDataSo.unlockableData.isUnlocked)
        //     {
        //         StartScan();
        //     }
        // }

        private void OnDamaged()
        {
            StopScan();
        }

        private void OnLevelUpgraded()
        {
            
        }

        private void ScanForEnemy()
        {
            CustomDebug.LogWarning("Optimize Scan For Enemy");
            
            enemyListSo.ClearList();
            
            var hitColliders = new Collider[maxInteractable];
            var numColliders = Physics.OverlapSphereNonAlloc(transform.position, scanRadius, hitColliders, targetLayer);
            

            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].TryGetComponent(out IEnemy enemy))
                {
                    enemyListSo.AddEnemyInRangeList(enemy);
                }
            }

            if (enemyListSo.AllEnemyCountInRange > 0)
            {
                InitFire();
            }
        }
        
        private void OnRepaired()
        {
            StartScan();
        }

        private void InitFire()
        {
            RotateArrowBase();
        }

        private void Fire()
        {
            var weapon = Instantiate(weaponSo.weapon, arrowBase);
            
            //weapon.transform.localPosition = Vector3.up * 2.45f; 
            
            weapon.InitWeapon(enemyListSo.GetFirstEnemy());

        }

        private void RotateArrowBase()
        {
            var direction = enemyListSo.GetFirstEnemy().position - arrowBase.position;
            direction.y = 0;
            var targetRotation = Quaternion.LookRotation(direction);
            
            arrowBase.DORotate(targetRotation.eulerAngles, 0.1f).OnComplete(Fire);
        }

        private void StartScan()
        {
            if (unlockableDataSo.unlockableData.isUnlocked && !unlockableDataSo.unlockableData.isDamaged)
            {
                _fireAble = true;
            
                CancelInvoke(nameof(ScanForEnemy));
                InvokeRepeating(nameof(ScanForEnemy), 1, 1);
            }
        }

        private void StopScan()
        {
            _fireAble = false;
            
            CancelInvoke(nameof(ScanForEnemy));
        }
    }
}
