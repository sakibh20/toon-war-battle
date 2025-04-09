using DG.Tweening;
using UnityEngine;

namespace skb_sec._Project.Scripts.Pick_System
{
    public class PickableCoin : Pickable
    {
        private Transform _targetTransform;
        protected override void PickUpEffect(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            DisableCollider();
            TweenCollect();
        }

        private void TweenCollect()
        {
            transform.parent = _targetTransform;
            transform.DOLocalJump(Vector3.up * 1f,0.3f,1, 0.1f).SetEase(Ease.Flash).OnComplete(OnCompleteTweenCollect);
        }

        private void OnCompleteTweenCollect()
        {
            OnCompleteCollect();
        }

        protected override void Appear()
        {
            objectTransform.DOKill();

            transform.parent = null;
            
            var targetPos = transform.position - Vector3.up;

            targetPos.x = transform.position.x + Random.Range(-maxDisplacement, maxDisplacement);
            targetPos.z = transform.position.z + Random.Range(-maxDisplacement, maxDisplacement);

            objectTransform.DOLocalJump(targetPos, 3.0f, 1,0.75f).SetEase(Ease.OutBounce).OnComplete(OnCompleteAppearTween);
        }
        private void OnCompleteAppearTween()
        {
            Invoke(nameof(EnablePickup), 0.5f);
        }
    }
}
