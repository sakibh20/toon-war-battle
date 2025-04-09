using System;
using Doozy.Engine.UI;
using skb_sec._Project.Scripts.Economy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Upgrade
{
    public class UpgradeUiManager : MonoBehaviour
    {
        [SerializeField] private UIView upgradeView;
        
        [SerializeField, Space] private TMP_Text upgradeItemTitle;
        [SerializeField] private TMP_Text upgradeItemInfo;
        [SerializeField] private TMP_Text upgradeItemCost;
        [SerializeField] private TMP_Text currentLevel;
        [SerializeField] private Image upgradeItemIcon;
        [SerializeField] private Image upgradeCostTypeItemIcon;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button hideUpgradeView;
        
        [SerializeField] private GameObject iconParent;
        
        [SerializeField, Space] private UpgradeManagerSO upgradeManagerSo;

        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            upgradeManagerSo.ShowUpgradeView += ShowUpgradeUi;
            hideUpgradeView.onClick.AddListener(HideUpgradeUi);
            upgradeButton.onClick.AddListener(Upgrade);
        }

        private void UnSubscribe()
        {
            upgradeManagerSo.ShowUpgradeView -= ShowUpgradeUi;
            hideUpgradeView.onClick.RemoveListener(HideUpgradeUi);
            upgradeButton.onClick.RemoveListener(Upgrade);
        }

        private void ShowUpgradeUi()
        {
            RefreshInformation();
            upgradeView.Show();
        }

        private void RefreshInformation()
        {
            if (upgradeManagerSo.maxLevel == upgradeManagerSo.currentLevel)
            {
                upgradeButton.interactable = false;
                upgradeItemCost.text = "Max LV";
                upgradeItemInfo.text = "";
                
                iconParent.SetActive(false);
            }
            else
            {
                upgradeButton.interactable = upgradeManagerSo.upgradeCostResource.Value >= upgradeManagerSo.upgradeCost;
                upgradeItemCost.text = upgradeManagerSo.upgradeCost.ToString();
                upgradeItemInfo.text = upgradeManagerSo.upgradeInfo;
                iconParent.SetActive(true);
            }

            upgradeCostTypeItemIcon.sprite = upgradeManagerSo.upgradeCostResource.resourceIcon;
            upgradeCostTypeItemIcon.preserveAspect = true;
            
            upgradeItemIcon.sprite = upgradeManagerSo.storageIcon;
            upgradeItemIcon.preserveAspect = true;
            
            upgradeItemTitle.text = upgradeManagerSo.title;
            currentLevel.text = "LV " + (upgradeManagerSo.currentLevel + 2);
        }
        
        private void Upgrade()
        {
            upgradeManagerSo.targetUpgradeable.Upgrade();

            RefreshInformation();
            
            HideUpgradeUi();
        }
        
        private void HideUpgradeUi()
        {
            upgradeView.Hide();
        }
        

        private void OnDisable()
        {
            UnSubscribe();
        }
    }
}
