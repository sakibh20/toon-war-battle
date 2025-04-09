using System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Exchange
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Exchange/Create Exchange Data", fileName = "Exchange Data S0"), Serializable]
    public class ExchangeDataSO : ScriptableObject
    {
        public int lastExchangedValue;

        public event Action OnExchangedComplete;

        public void FireOnExchangedComplete()
        {
            OnExchangedComplete?.Invoke();
        }
    }
}
