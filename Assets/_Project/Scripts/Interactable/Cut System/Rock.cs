using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    public class Rock : Cutables
    {
        private Transform _stone;
        private readonly Vector3 _shakeStrength = new Vector3(0.0001f, 0.0001f, 0.0001f);

        private Vector3 _lastScale;

        protected override void HitEffect()
        {
            Shake();
        }
        
        [Button]
        private void Shake()
        {
            if (_stone)
            {
                _stone.DOKill();
                
                _stone.localScale = _lastScale;
            }
            
            _stone = allParts[CurrentHealth].transform;

            _lastScale = _stone.localScale;
            
            _stone.DOShakeScale(0.1f, _shakeStrength,1).SetEase(Ease.InOutBounce);
        }
    }
}
