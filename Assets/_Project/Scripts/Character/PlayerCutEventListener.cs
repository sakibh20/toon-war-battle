using skb_sec._Project.Scripts.Interactable.Cut_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Character
{
    public class PlayerCutEventListener : CutAnimationEventListener
    {
        [SerializeField] private CutablesInRangeSO cutablesInRangeSo;
        [SerializeField] private ParticleSystem slashParticle;

        protected override void Hit()
        {
            cutablesInRangeSo.StartInteraction();
        }
        
        private  void SlashEffect()
        {
            slashParticle.Stop();
            slashParticle.Play();
        }
    }
}