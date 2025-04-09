using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public class UnderConstructionManager : MonoBehaviour
    {
        [SerializeField] private Transform pillars;
        [SerializeField] private Transform floor1;
        [SerializeField] private Transform floor2;
        [SerializeField] private Transform floor3;


        private Vector3 _initialPillarPos;
        private Vector3 _initialFloor1Pos;
        private Vector3 _initialFloor2Pos;
        private Vector3 _initialFloor3Pos;

        private void Awake()
        {
            _initialPillarPos = pillars.position;
            _initialFloor1Pos = floor1.position;
            _initialFloor2Pos = floor2.position;
            _initialFloor3Pos = floor3.position;
        }

        [Button]
        public void StartUnlockAnim()
        {
            pillars.DOMoveY(0, 0.3f).SetEase(Ease.OutBounce);
            floor1.DOMoveY(0, 0.5f).SetEase(Ease.OutBounce);
            floor2.DOMoveY(0, 0.8f).SetEase(Ease.OutBounce);
            floor3.DOMoveY(0, 1f).SetEase(Ease.OutBounce).OnComplete(ResetPos);
        }

        private void ResetPos()
        {
            pillars.position = _initialPillarPos;
            floor1.position = _initialFloor1Pos;
            floor2.position = _initialFloor2Pos;
            floor3.position = _initialFloor3Pos;
        }
    }
}
