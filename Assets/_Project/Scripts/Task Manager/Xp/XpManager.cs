using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Core.Pool_Manager.Script;
using skb_sec._Project.Scripts.Pick_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace skb_sec._Project.Scripts.Task_Manager.Xp
{
    public class XpManager : MonoBehaviour
    {
        [SerializeField, Required] private Button getRewordButton;
        [SerializeField, Required] private TMP_Text levelCount;
        [SerializeField, Required] private TMP_Text xpCount;
        [SerializeField, Required] private Image xpBar;

        [SerializeField, Required] private TaskDataManagerSO taskDataManagerSo;
        [SerializeField, Required] private XpDataManagerSO xpDataManager;

        [SerializeField] private float barFillTweenTime;
        [SerializeField] private Pickable xpBarObjectPrefab;

        private void Awake()
        {
            getRewordButton.onClick.AddListener(CollectReward);
            
            SetXpData();
            
            Subscribe();
        }

        private void Subscribe()
        {
            xpDataManager.OnLevelDataLoaded += SetXpData;
            xpDataManager.OnLevelUpdated += SetXpData;
        }

        private void UnSubscribe()
        {
            xpDataManager.OnLevelDataLoaded -= SetXpData;
            xpDataManager.OnLevelUpdated -= SetXpData;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        [Button]
        private void SetXpData()
        {
            levelCount.text = "LV " + (xpDataManager.currentLevel + 1);
            
            xpCount.text = xpDataManager.currentXpCount + "/" +
                           xpDataManager.allLevels[xpDataManager.currentLevel].requiredXpCount;
            
            var totalXp = xpDataManager.allLevels[xpDataManager.currentLevel].requiredXpCount;

            var fillAmount = xpDataManager.currentXpCount / (float)totalXp;

            TweenXpBar(fillAmount);
        }

        private void TweenXpBar(float target)
        {
            float value = xpBar.fillAmount;
            float targetValue = target;

            DOTween.To( () => value, x => value = x, targetValue, barFillTweenTime).OnUpdate(() =>
                {
                    xpBar.fillAmount = value;
                });
        }

        private void CollectReward()
        {
            InstantiateXpObjects();
            
            xpDataManager.currentXpCount +=
                taskDataManagerSo.selectedTask.onCompleteXp;
            
            Invoke(nameof(SetXpData),1f);
            
            if (xpDataManager.currentXpCount >= xpDataManager.allLevels[xpDataManager.currentLevel].requiredXpCount)
            {
                xpDataManager.currentLevel += 1;
                xpDataManager.FireOnLevelUpdated();
            }
            
            taskDataManagerSo.FireOnShowNextTask();
        }

        [Button]
        private void InstantiateXpObjects()
        {
            float range = 2.0f;

            for (int i = 0; i < 10; i++)
            {
                var pickable = Instantiate(xpBarObjectPrefab, ReferenceManager.instance.player);

                pickable.transform.localPosition = Vector3.up;
                
                pickable.InitPickable(PoolItemType.GoldPart, ReferenceManager.instance.player);
            }
        }
    }
}
