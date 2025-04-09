using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Storage;
using UnityEngine;

namespace skb_sec._Project.Scripts.Hireables
{
    public class HireableResourceCountWorldInfo : ResourceCountWorldInfo
    {
        private WorkerDataSO _workerDataSo;
        
        private Sequence _scoreScaleSequence;
        
        public override void InitStorageInfo(StorageUpgradeLevelsSO allLevelsData, int index)
        {
            
        }

        public override void InitHireableInfo(WorkerDataSO workerDataSo)
        {
            icon.preserveAspect = true;
            
            _workerDataSo = workerDataSo;
            icon.sprite = _workerDataSo.storingResourceSo.resourceIcon;
            
            UpdateInfo();
        }

        public override void UpdateInfo()
        {
            infoText.text = _workerDataSo.workerAllLevelsSo.storedValue  + "/" + _workerDataSo.workerAllLevelsSo.allUpgrades[_workerDataSo.workerAllLevelsSo.currentLevel].maxCapacity;
            
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
