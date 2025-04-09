using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Interactable/Cutables/CutablesData", fileName = "CutablesData")]
    public class CutablesDataSO : ScriptableObject
    {
        [ShowInInspector] public CutablesData cutablesData;
    }

    [Serializable]
    public struct CutablesData
    {
        public int scorePerParts;
        public float respawnTime;
    }
}
