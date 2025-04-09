using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.Barrack
{
    public class SoliderAnimationEventListener : MonoBehaviour
    {
        [SerializeField, Required] private Soldier.Soldier soldier;
        
        public void OnHit()
        {
            CustomDebug.Log("Handle Hit");
            soldier.HitOnTarget();
        }
    }
}
