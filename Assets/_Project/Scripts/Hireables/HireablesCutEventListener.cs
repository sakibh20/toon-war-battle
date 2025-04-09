using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Character;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    public class HireablesCutEventListener : CutAnimationEventListener
    {
        [SerializeField] private Hireables hireables;
        
        protected override void Hit()
        {
            hireables.HitCutable();
        }
        
        private  void SlashEffect()
        {
            
        }
    }
}
