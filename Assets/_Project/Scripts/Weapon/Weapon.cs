using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField, Required] protected WeaponDataSO data;
        private Transform _target;

        public void InitWeapon(Transform targetTransform)
        {
            _target = targetTransform;

            StartWeaponBehaviour();
        }

        private void StartWeaponBehaviour()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            var targetPos = _target.position + (Vector3.up * 1f);

            transform.DOMove(targetPos, 0.4f).SetEase(Ease.Linear).OnComplete(OnCompleteMove);
            
            SetRotation();
        }

        private void SetRotation()
        {
            var direction = _target.position + (Vector3.up * 1f) - transform.position;
            var targetRotation = Quaternion.LookRotation(direction);
            
            transform.rotation = targetRotation;
        }

        private void OnCompleteMove()
        {
            Destroy(gameObject);
        }
    }
}
