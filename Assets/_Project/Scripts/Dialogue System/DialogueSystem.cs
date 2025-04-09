using System;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Task_Manager.Xp;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Dialogue_System
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField, Required] private DialogueScript leftPersonDialoguePrefab;
        [SerializeField, Required] private DialogueScript rightPersonDialoguePrefab;
        
        [SerializeField, Required] private UIView dialogueView;
        [SerializeField, Required] private Transform dialogueParent;
        [SerializeField, Required] private Button next;
        
        [SerializeField, Required] private DialogueDataSO dialogueData;
        [SerializeField, Required] private XpDataManagerSO xpDataManagerSo;

        private int _currentDialogueIndex;

        private void Awake()
        {
            Subscribe();
        }
        

        private void Subscribe()
        {
            xpDataManagerSo.OnLevelDataLoaded += OnDataLoaded;
        }

        private void UnSubscribe()
        {
            xpDataManagerSo.OnLevelDataLoaded -= OnDataLoaded;
            next.onClick.RemoveListener(OnClickNext);
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnDataLoaded()
        {
            if (!dialogueData.showed)
            {
                next.onClick.AddListener(OnClickNext);
                
                Invoke(nameof(ShowDialogueView), 2.5f);
                
                Invoke(nameof(ShowNextDialogue), 3.0f);
            }
        }


        private void ShowDialogueView()
        {
            dialogueView.Show();
        }

        private void HideDialogueView()
        {
            dialogueView.Hide();
        }
        
        private void ShowNextDialogue()
        {
            if(_currentDialogueIndex >= dialogueData.dialogueList.Count) return;
            
            var dialogueObject = dialogueData.dialogueList[_currentDialogueIndex].leftPerson ? leftPersonDialoguePrefab : rightPersonDialoguePrefab;
            Sprite sprite = dialogueData.dialogueList[_currentDialogueIndex].leftPerson ? dialogueData.leftPersonImage : dialogueData.rightPersonImage;

            var dialogue = Instantiate(dialogueObject, dialogueParent);
            
            dialogue.SetInfo(sprite, dialogueData.dialogueList[_currentDialogueIndex].dialogueMessage);
        }

        [Button]
        private void OnClickNext()
        {
            _currentDialogueIndex += 1;

            if (_currentDialogueIndex >= dialogueData.dialogueList.Count)
            {
                HideDialogueView();
                dialogueData.showed = true;
                dialogueData.FireOnDialogueComplete();
                return;
            }
            
            ShowNextDialogue();
        }
    }
}
