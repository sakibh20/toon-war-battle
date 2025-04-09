using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Economy
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private Image resourceIcon;
        [SerializeField] private TextMeshProUGUI countText;

        private ResourceSO _resourceSo;
        private Sequence _scoreScaleSequence;
        private Transform _textTransform;

        private void ManageInternalReference()
        {
            _textTransform = countText.transform;
            _scoreScaleSequence = DOTween.Sequence();
        }

        public void Initialize(ResourceSO resourceSo)
        {
            ManageInternalReference();
            
            gameObject.name = "Resource-" + resourceSo.resourceName;
            _resourceSo = resourceSo;
            resourceIcon.sprite = resourceSo.resourceIcon;
            resourceIcon.preserveAspect = true;
            
            countText.SetText(ShortCount.Shorten(resourceSo.Value));
            ManageAppearance();
        }

        public void SetCountInstant(int currentValue)
        {
            countText.SetText(ShortCount.Shorten(currentValue));
            ManageAppearance();
            TweenScale();
        }

        [Button]
        private void TweenScale()
        {
            _scoreScaleSequence.Kill();
            _scoreScaleSequence = DOTween.Sequence();
            
            _textTransform.localScale = Vector3.one;
            
            _scoreScaleSequence.Append(_textTransform.DOScale(Vector3.one * 2f, 0.15f).SetEase(Ease.OutFlash));
            _scoreScaleSequence.Append(_textTransform.DOScale(Vector3.one, 0.07f).SetEase(Ease.OutFlash));
        }

        private void ManageAppearance()
        {
            gameObject.SetActive(_resourceSo.Value > 0);
        }
    }
}
