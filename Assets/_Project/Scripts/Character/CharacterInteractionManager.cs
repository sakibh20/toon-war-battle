using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Pick_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Character
{
    public class CharacterInteractionManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Pickable pickable))
            {
                CustomDebug.Log("pickable: "+pickable.name);
                pickable.PickUp(transform, true);
            }
        }

        // private void OnControllerColliderHit(ControllerColliderHit hit)
        // {
        // }
    }
}
