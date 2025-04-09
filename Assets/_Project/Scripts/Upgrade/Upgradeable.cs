using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Upgrade
{
    public abstract class Upgradeable : MonoBehaviour
    {
        [SerializeField] private UIView upgradeUiButtonView;
        [SerializeField] private Button upgradeUiButton;

        // private void Awake()
        // {
        //     Subscribe();
        // }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            upgradeUiButton.onClick.AddListener(ShowUpgradePanel);
        }

        private void UnSubscribe()
        {
            upgradeUiButton.onClick.RemoveListener(ShowUpgradePanel);
        }

        protected abstract void Show();

        public void ShowUpgradeButtonView()
        {
            upgradeUiButtonView.Show();
        }
        
        public void HideUpgradeButtonView()
        {
            upgradeUiButtonView.Hide();
        }
        
        [Button]
        public void ShowUpgradePanel()
        {
            CustomDebug.Log("ShowUpgradePanel");
            Show();
        }

        public void ManageAppearance()
        {
            ManageAppear();
        }

        protected abstract void ManageAppear();

        public abstract void Upgrade();

        private void OnDisable()
        {
            UnSubscribe();
        }
    }
}
