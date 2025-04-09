using System;
using System.Collections.Generic;
using _Project.Core.Custom_Debug_Log.Scripts;
using Sirenix.OdinInspector;
using skb_sec._Project.Core.Joystick_Unity_Character_Controller.Scripts;
using skb_sec._Project.Scripts.Dialogue_System;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Task_Manager.Xp;
using UnityEngine;

namespace skb_sec._Project.Scripts.Task_Manager
{
    public class TaskManager : MonoBehaviour
    {
        //public List<Tasks> allTasksList;
        
        [ShowInInspector] public List<Task> allTask;

        [SerializeField, Required] private TaskDataManagerSO taskDataManager;
        [SerializeField, Required] private XpDataManagerSO xpDataManagerSo;
        [SerializeField, Required] private DialogueDataSO dialogueDataSo;

        [SerializeField, Required] private ControllerInputSO controllerInputSo;
        

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
            xpDataManagerSo.OnLevelDataLoaded += OnDataLoaded;
            taskDataManager.OnShowNextTask += InvokeNextTask;
            taskDataManager.OnTaskCompleted += OnTaskCompleted;
        }

        private void UnSubscribe()
        {
            xpDataManagerSo.OnLevelDataLoaded -= OnDataLoaded;
            taskDataManager.OnShowNextTask -= InvokeNextTask;
            taskDataManager.OnTaskCompleted -= OnTaskCompleted;
        }

        [Button]
        private void OnValidate()
        {
            taskDataManager.allTasks.Clear();
            
            foreach (Task task in allTask)
            {
                taskDataManager.allTasks.Add(task.taskData);
            }
        }

        private void OnDataLoaded()
        {
            InitAppearance();
            
            if (dialogueDataSo.showed)
            {
                Invoke(nameof(InitNextTask), 3.0f);
            }
            else
            {
                dialogueDataSo.OnDialogueComplete += OnCompleteDialogue;
            }
        }

        private void OnCompleteDialogue()
        {
            Invoke(nameof(InitNextTask), 2.0f);
        }

        private void OnTaskCompleted()
        {
            taskDataManager.nextTaskIndex += 1;
        }


        [Button]
        private void ShowNextTask()
        {
            if(taskDataManager.nextTaskIndex >= allTask.Count) return;
            
            allTask[taskDataManager.nextTaskIndex].gameObject.SetActive(true);
            
            allTask[taskDataManager.nextTaskIndex].SetAsNextTask(OnTaskComplete, OnProgressTask);

            taskDataManager.selectedTask = allTask[taskDataManager.nextTaskIndex].taskData;
            
            DisableControllerInput();
            
            taskDataManager.FireOnTaskAssigned();
            
            Invoke(nameof(EnableControllerInput), 4.0f);
        }

        private void DisableControllerInput()
        {
            controllerInputSo.FireOnPauseControllerInput();
        }

        private void EnableControllerInput()
        {
            controllerInputSo.FireOnResumeControllerInput();
        }

        private void InitAppearance()
        {
            for (int i = 0; i < allTask.Count; i++)
            {
                allTask[i].gameObject.SetActive(i<taskDataManager.nextTaskIndex);
            }
        }

        private void InitNextTask()
        {
            //taskDataManager.nextTaskIndex -= 1;
            
            InvokeNextTask();
        }

        private void OnTaskComplete()
        {
            //CustomDebug.Log("Task Completed");

            taskDataManager.FireOnTaskCompleted();
            
            //InvokeNextTask();
        }

        private void OnProgressTask(int value)
        {
            taskDataManager.FireOnTaskStatusProgression(value);
        }

        private void InvokeNextTask()
        {
            if (taskDataManager.nextTaskIndex >= allTask.Count)
            {
                CustomDebug.Log("No new task to unlock");
                return;
            }

            Invoke(nameof(ShowNextTask), taskDataManager.nextTaskInvokeTime);
        }
    }
    
    
    [Serializable]
    public class Tasks
    {
        //public float taskInvokeTime;
        public Unlockables task;
    }
}
