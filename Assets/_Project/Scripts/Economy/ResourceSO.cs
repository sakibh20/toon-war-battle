using System;
using skb_sec._Project.Core.Pool_Manager.Script;
using skb_sec._Project.Scripts.Expense_Effect_System;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Economy
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Resources/Create New Resource", fileName = "NewResource")]
    public class ResourceSO : ScriptableObject
    {
        public string resourceName;
        public Sprite resourceIcon;
        public PoolItemType poolItemType;

        [Space]
        [SerializeField] private int resourceValue;
        [SerializeField] private float exchangeRatio;

        public event Action OnValueIncreased;
        
        public int Value 
        {
            get => resourceValue;
            set
            {
                if (resourceValue < value)
                {
                    OnValueIncreased?.Invoke();
                }
                resourceValue = value;
            }
        }
        
        public float ExchangeRatio => exchangeRatio;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                Debug.LogError("resource name should not be empty or null");
            }
        }
    }
}
