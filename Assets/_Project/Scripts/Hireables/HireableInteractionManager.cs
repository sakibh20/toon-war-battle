using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Pick_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    public class HireableInteractionManager : MonoBehaviour
    {
        [SerializeField, Required] private ResourceSO collectableResource;
        [SerializeField] private Scripts.Hireables.Hireables hireables;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Pickable pickable))
            {
                if (pickable.NotPickableByHired) return;
                if(!pickable.DoesResourceMatch(collectableResource)) return;
                
                pickable.PickUp(transform, false);
                hireables.AddResource();
            }
        }
    }
}
