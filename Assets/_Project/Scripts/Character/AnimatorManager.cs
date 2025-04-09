using _Project.Core.Custom_Debug_Log.Scripts;
using UnityEngine;

namespace skb_sec._Project.Scripts.Character
{
    public class AnimatorManager : MonoBehaviour
    {
        private Animator _characterAnimator;

        private static readonly int Lay = Animator.StringToHash("lay");
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Run = Animator.StringToHash("run");
        private static readonly int Cut = Animator.StringToHash("cut");

        private ReferenceManager _referenceManager;
        
        private void Awake()
        {
            _characterAnimator = GetComponent<Animator>();
        }

        public void RunAnimation()
        {
            DisableAll();

            _characterAnimator.SetBool(Run, true);
        }

        public void IdleAnimation()
        {
            DisableAll();

            _characterAnimator.SetBool(Idle, true);
        }
        
        public void LayAnimation()
        {
            DisableAll();

            _characterAnimator.SetBool(Lay, true);
        }
        
        public void CuttingAnimation()
        {
            DisableAll();

            _characterAnimator.SetBool(Cut, true);
        }

        private void DisableAll()
        {
            if (_characterAnimator == null)
            {
                _characterAnimator = GetComponent<Animator>();
            }
            foreach (AnimatorControllerParameter parameter in _characterAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    _characterAnimator.SetBool(parameter.name, false);
                }
            }
        }
    }
}
