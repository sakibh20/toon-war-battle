using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Core.Joystick_Unity_Character_Controller.Scripts
{
    [CreateAssetMenu(menuName = "SKB Custom SO/ControllerInput", fileName = "ControllerInput")]
    public class ControllerInputSO : ScriptableObject
    {
        [ShowInInspector] public InputInfo inputInfo;

        public event Action OnPauseControllerInput;
        public event Action OnResumeControllerInput;


        public virtual void FireOnPauseControllerInput()
        {
            OnPauseControllerInput?.Invoke();
        }

        public virtual void FireOnResumeControllerInput()
        {
            OnResumeControllerInput?.Invoke();
        }
    }

    public struct InputInfo
    {
        public float speedModifier;
        public float moveAngle;
    }
}
