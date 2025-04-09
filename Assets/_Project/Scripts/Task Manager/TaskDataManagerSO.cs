using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Interactable.Unlock_System;
using skb_sec._Project.Scripts.Task_Manager.All_Tasks;
using UnityEngine;

namespace skb_sec._Project.Scripts.Task_Manager
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Task Manager/Create Task Data Manager", fileName = "Task Data Manager SO")]
    public class TaskDataManagerSO : ScriptableObject
    {
        [ShowInInspector] public List<TaskDataSo> allTasks;
        [ReadOnly] public TaskDataSo selectedTask;
        
        public int nextTaskIndex;
        public int nextTaskInvokeTime;

        public event Action OnTaskAssigned;
        public event Action OnTaskCompleted;
        public event Action OnShowNextTask;

        public event Action<int> OnTaskStatusProgression;


        public void FireOnTaskAssigned()
        {
            OnTaskAssigned?.Invoke();
        }

        public void FireOnTaskCompleted()
        {
            OnTaskCompleted?.Invoke();
        }

        public void FireOnTaskStatusProgression(int value)
        {
            OnTaskStatusProgression?.Invoke(value);
        }

        public void FireOnShowNextTask()
        {
            OnShowNextTask?.Invoke();
        }
    }

}
