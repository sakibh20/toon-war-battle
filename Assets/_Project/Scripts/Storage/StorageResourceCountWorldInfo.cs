using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Hireables;
using UnityEngine;

namespace skb_sec._Project.Scripts.Storage
{
    public class StorageResourceCountWorldInfo : ResourceCountWorldInfo
    {
        private StorageUpgradeLevelsSO _allLevelsData;
        private int _index;
        
        private Sequence _scoreScaleSequence;

        public override void InitStorageInfo(StorageUpgradeLevelsSO allLevelsData, int index)
        {
            _allLevelsData = allLevelsData;
            _index = index;
            
            icon.sprite = _allLevelsData.allUpgrades[_allLevelsData.CurrentLevel].allIStorable[_index].resource.resourceIcon;
            icon.preserveAspect = true;

            UpdateInfo();
        }

        public override void InitHireableInfo(WorkerDataSO workerDataSo)
        {
            
        }


        public override void UpdateInfo()
        {
            infoText.text =_allLevelsData.allUpgrades[_allLevelsData.CurrentLevel].allIStorable[_index].storedValue  + "/" + 
                           _allLevelsData.allUpgrades[_allLevelsData.CurrentLevel].maxCapacity;
            
            _scoreScaleSequence = DOTween.Sequence();
            
            //TweenScale();
        }

        // [Button]
        // private void TweenScale()
        // {
        //     ResetSequence();
        //
        //     _scoreScaleSequence.Append(infoText.transform.DOScale(Vector3.one * 2f, 0.15f).SetEase(Ease.OutFlash));
        //     _scoreScaleSequence.Append(infoText.transform.DOScale(Vector3.one, 0.07f).SetEase(Ease.OutFlash));
        // }
        //
        // private void ResetSequence()
        // {
        //     _scoreScaleSequence.Kill();
        //     _scoreScaleSequence = DOTween.Sequence();
        //     
        //     infoText.transform.localScale = Vector3.one;
        // }
        //
        // private void OnDisable()
        // {
        //     ResetSequence();
        // }
    }
}
