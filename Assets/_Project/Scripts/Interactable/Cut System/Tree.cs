using DG.Tweening;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    public class Tree : Cutables
    {
        [SerializeField] private ParticleSystem treeLeavesParticle;
        private Transform _tree;
        private readonly Vector3 _shakeStrength = new Vector3(0f, 0, 8f);

        protected override void HitEffect()
        {
            Shake();
            if (CurrentHealth > 0)
            {
                LeavesParticle();
            }
        }
        
        private void Shake()
        {
            if (_tree)
            {
                _tree.DOKill();
                _tree.rotation = Quaternion.Euler(Vector3.zero);   
            }
            
            _tree = allParts[CurrentHealth].transform;
            _tree.DOShakeRotation(0.2f, _shakeStrength,80).SetEase(Ease.InOutBounce);
        }

        private void LeavesParticle()
        {
            treeLeavesParticle.Stop();
            treeLeavesParticle.Play();
        }
    }
}
