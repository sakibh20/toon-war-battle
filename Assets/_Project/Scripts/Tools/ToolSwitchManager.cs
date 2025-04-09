using _Project.Core.Custom_Debug_Log.Scripts;
using skb_sec._Project.Scripts.Interactable.Cut_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Tools
{
    public class ToolSwitchManager : MonoBehaviour
    {
        [SerializeField] private ToolSO toolSo;
        
        [SerializeField] private CutablesInRangeSO cutablesInRangeSo;
        [SerializeField] private Transform toolParent;
        
        private CuttingTools _targetCuttingTool;

        private CuttingTools _currentCuttingTool;

        private void Awake()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            toolSo.EquipCuttingTool += EquipTool;
            toolSo.UnEquipCuttingTool += UnEquipTool;
        }

        private void UnSubscribe()
        {
            toolSo.EquipCuttingTool -= EquipTool;
            toolSo.UnEquipCuttingTool -= UnEquipTool;
        }

        private void EquipTool()
        {
            //CustomDebug.Log("EquipTool");

            _targetCuttingTool = cutablesInRangeSo.targetCuttingTool.toolPrefab;

            GenerateTools();
        }
        
        private void UnEquipTool()
        {
            //CustomDebug.Log("UnEquipTool");
            Destroy(_currentCuttingTool.gameObject);
            
        }

        private void GenerateTools()
        {
            //CustomDebug.Log("GenerateTools");
            _currentCuttingTool = Instantiate(_targetCuttingTool, toolParent);
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
    }
}
