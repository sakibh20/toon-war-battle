using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using UnityEngine;

namespace skb_sec._Project.Scripts.Pick_System
{
    public class TreePart : Pickable
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
            
            transform.DOLocalJump(Vector3.up * 1.5f,0.3f,1, 0.3f).SetEase(Ease.Flash).OnComplete(OnCompleteJumpCollect);
        }

        private void OnCompleteJumpCollect()
        {
            transform.DOLocalMoveY(transform.localPosition.y - 0.5f, 0.1f).SetEase(Ease.Flash);
            transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Flash).OnComplete(OnCompleteCollect);
        }

        protected override void Appear()
        {
            objectTransform.DOKill();

            var targetPos = Vector3.zero;
            targetPos.x = Random.Range(-maxDisplacement, maxDisplacement);
            targetPos.z = Random.Range(-maxDisplacement, maxDisplacement);

            objectTransform.DOLocalJump(targetPos, 3.0f, 1,0.8f).SetEase(Ease.Flash).OnComplete(OnCompleteAppearTween);
        }

        private void OnCompleteAppearTween()
        {
            Invoke(nameof(EnablePickup), 0.01f);
        }
    }
}
