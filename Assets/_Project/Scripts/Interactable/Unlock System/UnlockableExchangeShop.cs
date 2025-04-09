namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnlockableExchangeShop : Unlockables
    {
        private ExchangeableUiManager _exchangeableUiManager;

        protected override void StartUnlockedInteraction()
        {
            ShowInteractionUI();
        }

        protected override void StopUnlockedInteraction()
        {
            if (!_exchangeableUiManager)
            {
                _exchangeableUiManager = FindObjectOfType<ExchangeableUiManager>();
            }
            _exchangeableUiManager.HideExchangeView();
        }

        protected override void ShowInteractionUI()
        {
            if (!_exchangeableUiManager)
            {
                _exchangeableUiManager = FindObjectOfType<ExchangeableUiManager>();
            }
            
            _exchangeableUiManager.ShowExchangeableItemUi();
        }

        protected override void HireWorkerOnUnlock()
        {
            
        }

        protected override void OnUnlock()
        {
            
        }
    }
}
