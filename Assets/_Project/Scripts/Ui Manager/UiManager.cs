using Doozy.Engine.UI;
using UnityEngine;

namespace skb_sec._Project.Scripts.Ui_Manager
{
    public class UiManager : MonoBehaviour
    {
        private ReferenceManager _referenceManager;
        private UIView _overlayView;

        private void Awake()
        {
            _referenceManager = ReferenceManager.instance;
            _overlayView = _referenceManager.overlayView;
            
            Invoke(nameof(HideOverlay), 1.5f);
        }

        private void HideOverlay()
        {
            _overlayView.Hide();
        }
    }
}
