using System;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Interactable.Exchange;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class ExchangeableItemUi : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        
        [SerializeField] private ExchangeDataSO exchangeDataSo;
        [SerializeField] private ResourceManagerSO resourceManagerSo;

        [SerializeField] private Button exchangeButton;
        
        [Space]
        [SerializeField] private TMP_Text exchangeableResourceText;
        [SerializeField] private Image exchangeableResourceImage;
        
        [Space]
        [SerializeField] private TMP_Text goldResourceText;
        [SerializeField] private Image goldResourceImage;
        
        private ResourceSO _resourceSo;
        private ResourceSO _goldResourceSo;

        private int _exchangeableCount;

        private void OnEnable()
        {
            exchangeButton.onClick.AddListener(Exchange);
            slider.onValueChanged.AddListener(SetTargetExchangeAble);
        }

        private void OnDisable()
        {
            exchangeButton.onClick.RemoveListener(Exchange);
            slider.onValueChanged.RemoveListener(SetTargetExchangeAble);
        }

        public void Initialize(ResourceSO resourceSo, ResourceSO goldResourceSo)
        {
            _resourceSo = resourceSo;
            _goldResourceSo = goldResourceSo;

            exchangeableResourceImage.sprite = _resourceSo.resourceIcon;
            exchangeableResourceImage.preserveAspect = true;
            
            goldResourceImage.sprite = _goldResourceSo.resourceIcon;
            goldResourceImage.preserveAspect = true;
        }

        public void OnShowExchangeUi()
        {
            slider.value = 1.0f;
            
            SetTargetExchangeAble(slider.value);
            UpdateAppearance();
        }

        private void UpdateValues()
        {
            exchangeableResourceText.text = ShortCount.Shorten(_exchangeableCount);
            goldResourceText.text = ShortCount.Shorten(ExchangedValue());
        }

        private void UpdateAppearance()
        {
            gameObject.SetActive(ExchangedValue() > 0);
        }

        private int ExchangedValue()
        {
            return (int)(_exchangeableCount * _resourceSo.ExchangeRatio);
        }

        private void SetTargetExchangeAble(float value)
        {
            _exchangeableCount = (int)(_resourceSo.Value * value);
            UpdateValues();
        }

        private void Exchange()
        {
            resourceManagerSo.DecreaseResource(_exchangeableCount, _resourceSo, true);
            resourceManagerSo.IncreaseResource(ExchangedValue(), _goldResourceSo, true);

            exchangeDataSo.lastExchangedValue = _exchangeableCount;
            exchangeDataSo.FireOnExchangedComplete();

            OnShowExchangeUi();
        }
    }
}
