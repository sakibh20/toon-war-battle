using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.Dialogue_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Dialogue System/Create Dialogue Data", fileName = "Dialogue Data SO")]
    public class DialogueDataSO : ScriptableObject
    {
        public List<Dialogue> dialogueList;

        public Sprite leftPersonImage;
        public Sprite rightPersonImage;

        public bool showed;

        public event Action OnDialogueComplete;

        public void FireOnDialogueComplete()
        {
            OnDialogueComplete?.Invoke();
        }
    }

    [Serializable]
    public class Dialogue
    {
        [TextArea] public string dialogueMessage;
        public bool leftPerson;
    }
}
