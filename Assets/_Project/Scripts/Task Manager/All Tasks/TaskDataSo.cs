using System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Task_Manager.All_Tasks
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Task Manager/Create Task Data", fileName = "TaskData-"), Serializable]
    public class TaskDataSo : ScriptableObject
    {
        public string description;
        public int onCompleteXp;
        public int countToComplete;
        public Sprite icon;

        // public event Action OnTaskStarted;
        // public event Action OnTaskCompleted;
        // public event Action<int> OnTaskProgress;
        //
        // public void FireOnTaskStarted()
        // {
        //     OnTaskStarted?.Invoke();
        // }
        //
        // public void FireOnTaskCompleted()
        // {
        //     OnTaskCompleted?.Invoke();
        // }
        //
        // public void FireOnTaskProgress(int value)
        // {
        //     OnTaskProgress?.Invoke(value);
        // }
    }
}
