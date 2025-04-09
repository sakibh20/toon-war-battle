using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Upgrade;
using UnityEngine;

namespace skb_sec._Project.Scripts.Town_Hall
{
    public class UpgradableTownHall : Upgradeable
    {
        [SerializeField, Space] private TownHallUpgradeLevelsSO allLevelsData;
        [SerializeField] private UpgradeManagerSO upgradeManagerSo;
        [SerializeField] private ResourceSO resourceSo;
        [SerializeField] private ResourceManagerSO resourceManagerSo;
        
        
        [SerializeField, Space] private Transform mainObject;
        [SerializeField, Space] private Transform costAreaObject;
        [SerializeField, Space] private GameObject costUi;
        
        [SerializeField] private UnderConstructionManager underConstructionManager;
        [SerializeField] private ParticleSystem poof;

        //[SerializeField] private List<GameObject> allLevelTownHall;
        //[SerializeField] private Storage.Storage storage;

        protected override void Show()
        {
            CustomDebug.Log("Show");
            SetValues();
            upgradeManagerSo.FireShowUpgradeView();
        }

        private void SetValues()
        {
            upgradeManagerSo.targetUpgradeable = this;
            upgradeManagerSo.upgradeCostResource = resourceSo;
            upgradeManagerSo.currentLevel = allLevelsData.CurrentLevel;
            upgradeManagerSo.maxLevel = allLevelsData.allUpgrades.Count-1;
            upgradeManagerSo.title = allLevelsData.title.ToUpper();
            upgradeManagerSo.storageIcon = allLevelsData.sprite;
            
            if (allLevelsData.allUpgrades.Count-1 > allLevelsData.CurrentLevel)
            {
                upgradeManagerSo.upgradeCost = allLevelsData.allUpgrades[allLevelsData.CurrentLevel+1].upgradeCost;

                upgradeManagerSo.upgradeInfo ="Power will be increased from " + allLevelsData.allUpgrades[allLevelsData.CurrentLevel].maxPower + " to "+
                                              allLevelsData.allUpgrades[allLevelsData.CurrentLevel+1].maxPower;
            }
        }

        protected override void ManageAppear()
        {
            ShowAppearAnim();
        }

        public override void Upgrade()
        {
            //CustomDebug.Log("Handle Town Hall Upgrade");
            allLevelsData.CurrentLevel = allLevelsData.CurrentLevel + 1;

            int newVal = resourceSo.Value - allLevelsData.allUpgrades[allLevelsData.CurrentLevel].upgradeCost;
            resourceManagerSo.SetResource(newVal, resourceSo, true);
            
            //storage.UpdateStorageWorldInfo();

            SetValues();

            //Invoke(nameof(ManageAppear), 0.5f);
        }

        private void ShowAppearAnim()
        {
            //DisableAll();
            
            costUi.SetActive(false);
            ShowCostArea();
            
            underConstructionManager.StartUnlockAnim();

            Invoke(nameof(AppearMainObject), 1.2f);
            Invoke(nameof(ShowPoof), 0.7f);
        }
        
        private void ShowCostArea()
        {
            costAreaObject.gameObject.SetActive(true);
            costAreaObject.localScale = Vector3.one;
        }
        
        [Button]
        private void AppearMainObject()
        {
            var appearTime = 0.5f;
            
            //mainObject.localPosition = Vector3.zero;
            mainObject.localScale = Vector3.zero;
            
            //ShowUpdatedTownHall();

            mainObject.gameObject.SetActive(true);

            costAreaObject.DOScale(Vector3.zero, appearTime).OnComplete(OnHideCostArea);
            
            mainObject.DOScale(Vector3.one * 1.5f, appearTime/2.0f).SetEase(Ease.OutExpo).OnComplete(OnCompleteScaleUp);
            
            //transform.DOLocalJump(Vector3.up * 1.5f,0.3f,1, 0.3f).SetEase(Ease.Flash).OnComplete(OnCompleteJumpCollect);
            mainObject.DOLocalJump(mainObject.localPosition, 0.2f,1, appearTime + 0.1f).SetEase(Ease.OutFlash);
        }

        // private void ShowUpdatedTownHall()
        // {
        //     if (allLevelsData.currentLevel > allLevelTownHall.Count-1)
        //     {
        //         allLevelTownHall[allLevelTownHall.Count-1].SetActive(true);
        //         return;
        //     }
        //     
        //     for (int i = 0; i < allLevelTownHall.Count; i++)
        //     {
        //         if (i == allLevelsData.currentLevel)
        //         {
        //             allLevelTownHall[i].SetActive(true);
        //         }
        //     }
        // }
        //
        // private void DisableAll()
        // {
        //     for (int i = 0; i < allLevelTownHall.Count; i++)
        //     {
        //         allLevelTownHall[i].SetActive(false);
        //     }
        // }
        
        private void OnCompleteScaleUp()
        {
            mainObject.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
        }

        private void ShowPoof()
        {
            poof.Play();
        }
        
        private void OnHideCostArea()
        {
            costAreaObject.gameObject.SetActive(false);
            costAreaObject.localScale = Vector3.one;
            
            costUi.SetActive(true);
        }
    }
}
