using UnityEngine;

namespace skb_sec._Project.Scripts.Barrack
{
    public class SoldierAnimatorManager : MonoBehaviour
    {
        private Animator _soldierAnimator;

        private static readonly int Attack = Animator.StringToHash("attack");
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Run = Animator.StringToHash("run");
        private static readonly int March = Animator.StringToHash("march");

        private ReferenceManager _referenceManager;
        
        private void Awake()
        {
            _soldierAnimator = GetComponent<Animator>();
        }

        public void RunAnimation()
        {
            DisableAll();

            _soldierAnimator.SetBool(Run, true);
        }

        public void IdleAnimation()
        {
            DisableAll();

            _soldierAnimator.SetBool(Idle, true);
        }
        
        public void AttackAnimation()
        {
            DisableAll();

            _soldierAnimator.SetBool(Attack, true);
        }
        
        public void MarchAnimation()
        {
            DisableAll();

            _soldierAnimator.SetBool(March, true);
        }

        private void DisableAll()
        {
            if (_soldierAnimator == null)
            {
                _soldierAnimator = GetComponent<Animator>();
            }
            foreach (AnimatorControllerParameter parameter in _soldierAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    _soldierAnimator.SetBool(parameter.name, false);
                }
            }
        }
    }
}
