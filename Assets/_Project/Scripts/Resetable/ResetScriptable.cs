using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace skb_sec._Project.Scripts.Resetable
{
    public class ResetScriptable : MonoBehaviour
    {
        [SerializeField] private List<DataManager.DataManager> allDataManager;
        [Button]
        private void ResetAll()
        {
            for (int i = 0; i < allDataManager.Count; i++)
            {
                allDataManager[i].ClearData();
            }
        }
    }
}
