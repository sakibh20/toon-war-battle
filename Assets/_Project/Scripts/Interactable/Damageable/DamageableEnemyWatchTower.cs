using _Project.Core.Custom_Debug_Log.Scripts;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Damageable
{
    public class DamageableEnemyWatchTower : Damageable
    {
        protected override void HandleOnDamaged()
        {
            CustomDebug.LogWarning("Handle Damage");
        }
    }
}
