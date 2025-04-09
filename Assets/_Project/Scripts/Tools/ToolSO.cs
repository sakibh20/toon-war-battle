using System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Tools
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Tools/Create New Tool", fileName = "NewTool")]
    public class ToolSO : ScriptableObject
    {
        public event Action EquipCuttingTool;
        public event Action UnEquipCuttingTool;

        public void FireEquipCuttingTool()
        {
            EquipCuttingTool?.Invoke();
        }

        public void FireUnEquipCuttingTool()
        {
            UnEquipCuttingTool?.Invoke();
        }
    }
}
