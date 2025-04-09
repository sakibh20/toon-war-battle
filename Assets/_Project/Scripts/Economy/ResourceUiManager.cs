using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Storage;
using skb_sec._Project.Scripts.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Economy
{
    public class ResourceUiManager : MonoBehaviour
    {
        [SerializeField] private ResourceUI resourceUiPrefab;
        [SerializeField] private Transform resourceUiParent;

        [SerializeField, Required] private TMP_Text totalResourceText;
        [SerializeField, Required] private Upgradeable upgradableBag;
        

        [SerializeField, Required] private StorageUpgradeLevelsSO allLevelsData;
        
        [SerializeField] private ResourceManagerSO resourceManagerSo;

        private Dictionary<ResourceSO, ResourceUI> _resourceDictionary;

        private bool _showingBagUpgradeView;

        private void Awake()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            resourceManagerSo.ResourceUpdated += OnResourceUpdated;
            resourceManagerSo.ResourceLoaded += OnDataLoaded;

            resourceManagerSo.BagFull += OnBagFull;
            resourceManagerSo.BagNotFull += OnBagNotFull;
        }

        private void UnSubscribe()
        {
            resourceManagerSo.ResourceUpdated -= OnResourceUpdated;
            resourceManagerSo.ResourceLoaded -= OnDataLoaded;
            
            resourceManagerSo.BagFull -= OnBagFull;
            resourceManagerSo.BagNotFull -= OnBagNotFull;
        }
        
        private void OnBagFull()
        {
            upgradableBag.ShowUpgradeButtonView();
            _showingBagUpgradeView = true;
        }
        
        [Button]
        private void OnBagNotFull()
        {
            if(!_showingBagUpgradeView) return;
            
            upgradableBag.HideUpgradeButtonView();
        }

        private void OnDataLoaded()
        {
            InitDictionary();
            SetTotalResourceCount();

            if (!resourceManagerSo.WithinBagCapacity(0))
            {
                OnBagFull();
            }
        }

        private void InitDictionary()
        {
            _resourceDictionary = new Dictionary<ResourceSO, ResourceUI>();

            for (int i = 0; i < resourceManagerSo.allResourceSo.Count; i++)
            {
                if(_resourceDictionary.ContainsKey(resourceManagerSo.allResourceSo[i]))
                {
                    CustomDebug.LogError("Duplicate Key Found. Returning.");
                    return;
                }

                var resourceUi = Instantiate(resourceUiPrefab, resourceUiParent);
                resourceUi.Initialize(resourceManagerSo.allResourceSo[i]);
                _resourceDictionary.Add(resourceManagerSo.allResourceSo[i], resourceUi);
            }
        }

        private void OnResourceUpdated(int currentValue, ResourceSO resourceSo)
        {
            UpdateUiInstant(currentValue, resourceSo);

            SetTotalResourceCount();
        }
        
        private void UpdateUiInstant(int currentValue, ResourceSO resourceSo)
        {
            if(_resourceDictionary.ContainsKey(resourceSo))
            {
                _resourceDictionary[resourceSo].SetCountInstant(currentValue);
            }
        }

        private void SetTotalResourceCount()
        {
            int max = allLevelsData.allUpgrades[allLevelsData.CurrentLevel].maxCapacity;
            totalResourceText.text = resourceManagerSo.TotalResource() + "/" + max;
        }
    }
}
