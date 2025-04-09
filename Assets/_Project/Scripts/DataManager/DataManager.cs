using UnityEngine;

namespace skb_sec._Project.Scripts.DataManager
{
    public abstract class DataManager : MonoBehaviour
    {
        [SerializeField] protected string fileName;
        
        protected void Awake()
        {
            Subscribe();
        }

        private void Start()
        {
            GetData();
        }

        private void OnDisable()
        {
            UnSubscribe();
            
            UpdateResource();
        }

        protected abstract void GetData();
        
        protected void OnResourceUpdated()
        {
            UpdateResource();
        }

        private void UpdateResource()
        {
            GatherResourceData();
            SaveData();
        }
        

        protected abstract void Subscribe();

        protected abstract void UnSubscribe();

        protected abstract void GatherResourceData();
        protected abstract void SaveData();

        public abstract void ClearData();
    }
}
