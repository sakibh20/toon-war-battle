using System;
using DG.Tweening;
using skb_sec._Project.Core.Joystick_Unity_Character_Controller.Scripts;
using skb_sec._Project.Scripts.Dialogue_System;
using skb_sec._Project.Scripts.Interactable;
using skb_sec._Project.Scripts.Interactable.Cut_System;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Task_Manager.Xp;
using skb_sec._Project.Scripts.Tools;
using UnityEngine;

namespace skb_sec._Project.Scripts.Character
{
    public class CharacterStateManager : MonoBehaviour
    {
        public DialogueDataSO dialogueDataSo;
        public XpDataManagerSO xpDataManagerSo;
        public ControllerInputSO controllerInputSo;
        public InteractablesInRangeSO cutablesInRangeSo;
        public InteractablesInRangeSO unlockablesInRangeSo;
        public ToolSO toolSo;
        public AnimatorManager characterAnimatorManager;

        private CharacterStateBase _characterCurrentState;
        private CharacterStateFactory _stateFactory;

        public CharacterStateBase CharacterCurrentState
        {
            get => _characterCurrentState;
            set => _characterCurrentState = value;
        }
        
        private void Awake()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            cutablesInRangeSo.ListUpdated += LookAtTarget;
            xpDataManagerSo.OnLevelDataLoaded += InitState;
        }

        private void UnSubscribe()
        {
            xpDataManagerSo.OnLevelDataLoaded -= InitState;
            cutablesInRangeSo.ListUpdated -= LookAtTarget;
            dialogueDataSo.OnDialogueComplete -= StartIdleState;
        }
        
        private void InitState()
        {
            _stateFactory = new CharacterStateFactory(this);

            if (!dialogueDataSo.showed)
            {
                dialogueDataSo.OnDialogueComplete += StartIdleState;
                characterAnimatorManager.LayAnimation();
            }
            else
            {
                StartIdleState();
            }
        }

        private void StartIdleState()
        {
            _characterCurrentState = _stateFactory.CharacterIdle();

            _characterCurrentState.EnterState();
        }

        private void Update()
        {
            if (_characterCurrentState != null)
            {
                _characterCurrentState.UpdateStates();
            }
        }

        public void LookAtTarget()
        {
            var targetTransform = cutablesInRangeSo.GetFirstInteractable();
            
            if(targetTransform == null) return;
            
            //characterAnimatorManager.RunAnimation();
            Vector3 targetAngle = Quaternion.LookRotation(targetTransform.position - transform.position).eulerAngles;
            targetAngle.x = 0;
            targetAngle.z = 0;

            transform.DORotate(targetAngle, 0.2f).OnComplete(OnCompleteLookAt);
        }
        
        private void OnCompleteLookAt()
        {
            //characterAnimatorManager.CuttingAnimation();
        }
    }
}
