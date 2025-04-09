using System;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.Utils.ColorModels;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Barrack.Soldier;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Expense_Effect_System;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Storage;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Barrack
{
    public class BarrackSlotManager : MonoBehaviour
    {
        [SerializeField] private UpgradeDataManagerSO upgradeDataManagerSo;
        [SerializeField] protected StorageUpgradeLevelsSO allLevelsData;
        [SerializeField] protected ResourceManagerSO resourceManagerSo;
        [SerializeField] private UnlockableDataSO unlockableStorage;

        [SerializeField, Required] private Soldier.Soldier playerSoldierPrefab;
        [SerializeField, Required] private Transform soldierParent;
        
        [SerializeField, Required] private SoldierGenerateTimeBar soldierGenerateTimeBarPrefab;
        [SerializeField, Required] private Transform infoPanel;
        [SerializeField, Required] private float generationTime;
        
        
        [SerializeField, Required] private ParticleSystem magicAppear;
        [SerializeField, Required] private SoldierManagerSO soldierManagerSo;
        
        private SoldierGenerateTimeBar _generatedTimeBar;

        //private Coroutine _transferRoutine;

        private int _generatedSolderCount;
        private Tween _scoreTween;

        [SerializeField] private List<Transform> allSlots;

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            upgradeDataManagerSo.StorageDataLoaded += OnUnlockableDataRefreshed;
            
            unlockableStorage.Unlocked += OnUnlockableDataRefreshed;
        }
        
        private void UnSubscribe()
        {
            upgradeDataManagerSo.StorageDataLoaded -= OnUnlockableDataRefreshed;
            unlockableStorage.Unlocked -= OnUnlockableDataRefreshed;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnUnlockableDataRefreshed()
        {
            InitSlotAppearance();
            
            if (unlockableStorage.unlockableData.isUnlocked)
            {
                _generatedTimeBar = Instantiate(soldierGenerateTimeBarPrefab, infoPanel);
                
                StartGeneration();
            }
        }

        private void StartGeneration()
        {
            InvokeRepeating(nameof(GenerateOneSolder),generationTime, generationTime);
            _generatedTimeBar.StartProgressbar(generationTime);
        }

        [Button]
        private void InitSlotAppearance()
        {
            if(!unlockableStorage.unlockableData.isUnlocked) return;
            
            DisableAll();

            for (int i = 0; i < allLevelsData.allUpgrades[allLevelsData.CurrentLevel].maxCapacity; i++)
            {
                if(i >= allSlots.Count) return;
                
                allSlots[i].gameObject.SetActive(true);
            }
        }

        private void DisableAll()
        {
            for (int i = 0; i < allSlots.Count; i++)
            {
                allSlots[i].gameObject.SetActive(false);
            }
        }

        public void UpdateSlotAppearance()
        {
            InitSlotAppearance();
            StartGeneration();
        }

        [Button]
        private void GenerateOneSolder()
        {
            if (_generatedSolderCount >= allLevelsData.allUpgrades[allLevelsData.CurrentLevel].maxCapacity)
            {
                return;
            }

            magicAppear.transform.position = soldierParent.position + (Vector3.up*0.5f);
            
            magicAppear.Play();
            
            _generatedTimeBar.StartProgressbar(generationTime);

            Soldier.Soldier playerSoldier = Instantiate(playerSoldierPrefab, soldierParent);
            
            //soldier.transform.localPosition = Vector3.zero;
            
            playerSoldier.SetDestination(allSlots[_generatedSolderCount].position);
            playerSoldier.initialPosition = allSlots[_generatedSolderCount].position;
            //playerSoldier.transform.position = allSlots[_generatedSolderCount].position;
            
            //soldier.SetDestination(allSlots[_generatedSolderCount].position);

            _generatedSolderCount += 1;
            
            soldierManagerSo.Enlist(playerSoldier);

            if (_generatedSolderCount >= allLevelsData.allUpgrades[allLevelsData.CurrentLevel].maxCapacity)
            {
                CancelInvoke(nameof(GenerateOneSolder));
                _generatedTimeBar.ResetBar();
            }
        }
    }
}
