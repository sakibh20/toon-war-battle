using System;
using skb_sec._Project.Scripts.Hireables;
using skb_sec._Project.Scripts.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Storage
{
    public abstract class ResourceCountWorldInfo : MonoBehaviour
    {
        [SerializeField] protected Image icon;
        [SerializeField] protected TMP_Text infoText;

        public abstract void InitStorageInfo(StorageUpgradeLevelsSO allLevelsData, int index);
        public abstract void InitHireableInfo(WorkerDataSO workerDataSo);

        public abstract void UpdateInfo();
    }
}
