using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using UnityEngine;

namespace skb_sec._Project.Scripts.Interactable.Cut_System
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Hireable/Create All Cutable Holder", fileName = "AllCutablesData")]
    public class AlICutableSO : ScriptableObject
    {
        [ShowInInspector] private Dictionary<ResourceSO, List<Cutables>> _allItems = new Dictionary<ResourceSO, List<Cutables>>();
        
        public void Enlist(ResourceSO resource, Cutables cutable)
        {
            if (_allItems.ContainsKey(resource))
            {
                var list = _allItems[resource];
                
                if (!list.Contains(cutable))
                {
                    list.Add(cutable);
                }
            }
            else
            {
                var list = new List<Cutables> { cutable };
                _allItems.Add(resource, list);
            }
        }

        public void RemoveFromList(ResourceSO resource, Cutables cutable)
        {
            if (_allItems.ContainsKey(resource))
            {
                var list = _allItems[resource];
                
                if (list.Contains(cutable))
                {
                    list.Remove(cutable);
                }
            }
        }

        public Cutables GetNearestItem(ResourceSO resource, Transform workerTransform)
        {
            if (_allItems.ContainsKey(resource))
            {
                var list = _allItems[resource];
                
                if (list.Count > 0)
                {
                    var index = 0;
                    var minDist = 1000000.0f;
                    for (int i = 0; i < list.Count; i++)
                    {
                        var distance = Vector3.Distance(workerTransform.position, list[i].transform.position);

                        index = distance < minDist ? i : index;
                        minDist = distance < minDist ? distance : minDist;
                    }
                    return list[index];
                }

                DoesNotExist();
            }

            DoesNotExist();
            return null;
        }


        private void DoesNotExist()
        {
            CustomDebug.LogError("No item available with that resource");
        }
    }
}
