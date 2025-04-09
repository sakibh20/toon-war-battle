using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Dialogue_System
{
    public class DialogueScript : MonoBehaviour
    {
        [SerializeField, Required] private Image image;
        [SerializeField, Required] private TMP_Text dialogueText;

        public void SetInfo(Sprite sprite, string dialogue)
        {
            image.sprite = sprite;
            image.preserveAspect = true;

            dialogueText.text = dialogue;
        }
    }
}
