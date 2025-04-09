using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Barrack
{
    public class SoldierGenerateTimeBar : MonoBehaviour
    {
        [SerializeField, Required] private Image fillBarImage;
        
        private float _time;
        
        public void StartProgressbar(float time)
        {
            _time = time;

            ReactivateSelf();
            
            Progress();
        }

        public void ResetBar()
        {
            fillBarImage.fillAmount = 0;
            
            DeactivateSelf();
        }

        private void DeactivateSelf()
        {
            gameObject.SetActive(false);
        }

        private void ReactivateSelf()
        {
            gameObject.SetActive(true);
        }
        
        private void Progress()
        {
            var startValue = 0.0f;
            var endValue = 1.0f;
            
            DOTween.To(() => startValue, x => startValue = x, endValue, _time).SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    fillBarImage.fillAmount = startValue;
                });
        }
    }
}
