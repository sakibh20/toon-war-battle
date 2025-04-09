using System;
using DG.Tweening;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.IGDC
{
    public class FirstVideoSceneSetup : MonoBehaviour
    {
        public GameObject secondCam;
        public GameObject dictator;
        public UIView dialogueView;
        public UIView inGameView;


        private Vector3 _dictatorCurrentScale;
        private void Start()
        {
            _dictatorCurrentScale = dictator.transform.localScale;
            dictator.transform.localScale = Vector3.zero;
            
            Invoke(nameof(StartSequence), 1.0f);
            Invoke(nameof(HideAll), 6.0f);
        }
        
        
        [Button]
        private void StartSequence()
        {
            secondCam.SetActive(true);
            
            TweenDictator();
            
            Invoke(nameof(ShowDialogue), 0.5f);
        }

        private void TweenDictator()
        {
            dictator.transform.DOScale(_dictatorCurrentScale, 0.3f).SetEase(Ease.OutFlash);
        }
        private void ShowDialogue()
        {
            dialogueView.Show();
            inGameView.Hide();
        }

        
        [Button]
        private void HideAll()
        {
            dictator.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutFlash);
            
            dialogueView.Hide();
            inGameView.Show();
            
            Invoke(nameof(DisableSecondCam), 1.0f);
        }

        private void DisableSecondCam()
        {
            secondCam.SetActive(false);
        }
    }
}
