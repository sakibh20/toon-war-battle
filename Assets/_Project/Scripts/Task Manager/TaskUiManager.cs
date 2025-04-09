using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using Doozy.Engine.Progress;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Task_Manager.All_Tasks;
using skb_sec._Project.Scripts.Task_Manager.Xp;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Task_Manager
{
    public class TaskUiManager : MonoBehaviour
    {
        [SerializeField, Required] private UIView taskViewParentView;
        [SerializeField, Required] private UIView taskInfoView;
        [SerializeField, Required] private UIView rewardView;

        [SerializeField, Required] private TMP_Text taskObjectiveText;
        [SerializeField, Required] private TMP_Text countText;
        [SerializeField, Required] private Image icon;
        [SerializeField, Required] private Image fillBarImage;
        
        
        [SerializeField, Required] private TaskDataManagerSO taskDataManager;
        [SerializeField, Required] private XpDataManagerSO xpDataManager;

        private TaskDataSo _nextTask;

        private int _lastTaskIndex;

        private void Awake()
        {
            Subscribe();
        }
        

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            taskDataManager.OnTaskAssigned += ShowTaskInfoView;
            taskDataManager.OnTaskCompleted += OnCompleteTask;
            taskDataManager.OnTaskStatusProgression += UpdateTaskViewInfo;
        }

        private void UnSubscribe()
        {
            taskDataManager.OnTaskAssigned -= ShowTaskInfoView;
            taskDataManager.OnTaskCompleted -= OnCompleteTask;
            taskDataManager.OnTaskStatusProgression -= UpdateTaskViewInfo;
        }
        
        [Button]
        private void ShowTaskInfoView()
        {
            _lastTaskIndex = taskDataManager.nextTaskIndex;
            
            SetTaskViewInfo();
            taskInfoView.Show();
            rewardView.Hide();
        }

        private void HideTaskInfoView()
        {
            taskInfoView.Hide();
        }

        private void HideTaskInfoParentView()
        {
            taskViewParentView.Hide();
        }

        private void OnCompleteTask()
        {
            CompleteTaskInfoUi();
            
            Invoke(nameof(HideTaskInfoView), 2.0f);
            Invoke(nameof(ShowRewardView), 2.0f);
        }        
        private void ShowRewardView()
        {
            rewardView.Show();
        }

        private void SetTaskViewInfo()
        {
            if (_lastTaskIndex >= taskDataManager.allTasks.Count)
            {
                HideTaskInfoParentView();
                return;
            }
            
            _nextTask = taskDataManager.allTasks[_lastTaskIndex];
            
            icon.sprite = taskDataManager.allTasks[_lastTaskIndex].icon;
            taskObjectiveText.text = _nextTask.description;
            
            var totalTask = taskDataManager.allTasks[_lastTaskIndex].countToComplete;
            countText.text = "0/" + totalTask;
            
            fillBarImage.fillAmount = 0.0f;
        }

        private void CompleteTaskInfoUi()
        {
            if(_lastTaskIndex >= taskDataManager.allTasks.Count) return;
            
            var totalTask = taskDataManager.allTasks[_lastTaskIndex].countToComplete;
            countText.text = totalTask + "/" + totalTask;
            
            fillBarImage.fillAmount = 1.0f;
        }

        private void UpdateTaskViewInfo(int value)
        {
            var totalTask = taskDataManager.allTasks[_lastTaskIndex].countToComplete;

            countText.text = value + "/" + totalTask;
            
            var fillAmount = value / (float)totalTask;
            
            fillBarImage.fillAmount = fillAmount;
        }
    }
}
