using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Barrack.Soldier;
using UnityEngine;

namespace skb_sec._Project.Scripts.Weapon
{
    public class WatchTowerArrowWeapon : Weapon
    {
        private void OnTriggerEnter(Collider other)
        {
            CustomDebug.Log("OnTriggerEnter");
            if (other.TryGetComponent(out Soldier soldier))
            {
                CustomDebug.Log("EnemySoldier");
                soldier.TakeDamage(data.damage);
            }
        }
    }
}
