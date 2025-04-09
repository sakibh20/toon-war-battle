using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class ExchangeableUiManager : MonoBehaviour
    {
        [SerializeField] private UIView exchangeView;
        [SerializeField] private Button doneButton;
        
        [SerializeField] private ExchangeableItemUi exchangeableUiItemPrefab;
        [SerializeField] private Transform exchangeableUiParent;
        [Space]
        [SerializeField] private ResourceManagerSO resourceManagerSo;
        [SerializeField] private ResourceSO goldResourceSo;

        private Dictionary<ResourceSO, ExchangeableItemUi> _resourceDictionary;

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
            resourceManagerSo.ResourceLoaded += InitDictionary;
            doneButton.onClick.AddListener(HideExchangeView);
        }

        private void UnSubscribe()
        {
            resourceManagerSo.ResourceLoaded -= InitDictionary;
            doneButton.onClick.RemoveListener(HideExchangeView);
        }

        private void InitDictionary()
        {
            _resourceDictionary = new Dictionary<ResourceSO, ExchangeableItemUi>();

            for (int i = 0; i < resourceManagerSo.allResourceSo.Count; i++)
            {
                if(resourceManagerSo.allResourceSo[i] == goldResourceSo) continue;
                
                if(_resourceDictionary.ContainsKey(resourceManagerSo.allResourceSo[i]))
                {
                    CustomDebug.LogError("Duplicate Key Found. Returning.");
                    return;
                }

                var resourceUi = Instantiate(exchangeableUiItemPrefab, exchangeableUiParent);
                resourceUi.Initialize(resourceManagerSo.allResourceSo[i], goldResourceSo);
                _resourceDictionary.Add(resourceManagerSo.allResourceSo[i], resourceUi);
            }
        }

        public void ShowExchangeableItemUi()
        {
            exchangeView.Show();
            
            foreach (ResourceSO key in _resourceDictionary.Keys)
            {
                _resourceDictionary[key].OnShowExchangeUi();
            }
        }

        public void HideExchangeView()
        {
            exchangeView.Hide();
        }
    }
}
