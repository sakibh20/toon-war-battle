using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Expense_Effect_System;
using skb_sec._Project.Scripts.Path_Find;
using skb_sec._Project.Scripts.Task_Manager;
using skb_sec._Project.Scripts.Tools;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace skb_sec._Project.Scripts.Interactable.Unlock_System
{
    public abstract class Unlockables : Task, IInteractable
    {
        [Space(30)]
        [SerializeField] private ResourceManagerSO resourceManagerSo;
        [SerializeField] private UnlockableManagerSO unlockableManagerSo;
        [SerializeField] private UnlockableDataSO unlockableDataSo;

        [SerializeField, Space] private TMP_Text costText;
        [SerializeField, Space] private Image icon;
        [SerializeField, Space] private Transform mainObject;
        [SerializeField, Space] private Transform costAreaObject;

        [SerializeField] private bool unlockableArea;
        [SerializeField, HideIf("unlockableArea")] private UnderConstructionManager underConstructionManager;
        
        [SerializeField, HideIf("unlockableArea")] private ParticleSystem poof;

        private int _requiredResource;
        private int _useResource;

        protected Collider newObjectCollider;

        private const float TWEEN_TIME_FOR100 = 1.0f;
        private const float MIN_TWEEN_TIME = 0.5f;
        private Tween _scoreTween;

        private void Awake()
        {
            unlockableManagerSo.UnlockableLoaded += OnUnlockableLoaded;
        }

        private void OnDisable()
        {
            unlockableManagerSo.UnlockableLoaded -= OnUnlockableLoaded;
        }

        private void Start()
        {
            OnUnlockableLoaded();
        }

        private void OnUnlockableLoaded()
        {
            if (unlockableDataSo.unlockableData.isUnlocked)
            {
                Unlock();
            }

            else
            {
                InitializeValues();
            }
        }

        private void InitializeValues()
        {
            costText.SetText(ShortCount.Shorten(unlockableDataSo.unlockableData.unlockCost-unlockableDataSo.unlockableData.alreadyInvested));
            icon.sprite = unlockableDataSo.requiredResourceSo.resourceIcon;
        }

        protected abstract void StartUnlockedInteraction();
        protected abstract void StopUnlockedInteraction();
        
        protected abstract void ShowInteractionUI();

        public void StartInteraction()
        {
            if(unlockableDataSo.unlockableData.isUnlocked)
            {
                StartUnlockedInteraction();
            }
            else if (unlockableDataSo.requiredResourceSo.Value > 0)
            {
                Invoke(nameof(StartUnlock), 1.0f);
            }
        }

        public void StopInteraction()
        {
            CancelInvoke(nameof(StartUnlock));
            
            _scoreTween.Kill();
            OnStoppedScoreTween();
            StopUnlockedInteraction();
        }

        public Transform ReturnTransform()
        {
            return transform;
        }

        private void StartUnlock()
        {
            _requiredResource = unlockableDataSo.unlockableData.unlockCost -
                                unlockableDataSo.unlockableData.alreadyInvested;


            _useResource = unlockableDataSo.requiredResourceSo.Value >= _requiredResource ? 
                _requiredResource : unlockableDataSo.requiredResourceSo.Value;

            TweenScore();
        }

        private void TweenScore()
        {
            var initialCount = unlockableDataSo.requiredResourceSo.Value;
            var alreadyInvested = unlockableDataSo.unlockableData.alreadyInvested;
            
            int value = initialCount;
            int endValue = initialCount - _useResource;
            
            var tweenTime = TWEEN_TIME_FOR100 * (Mathf.Abs(endValue - value)/100.0f);

            tweenTime = tweenTime < MIN_TWEEN_TIME ? MIN_TWEEN_TIME : tweenTime;
            
            InvokeRepeating(nameof(Expense), 0, 0.1f);

            _scoreTween = DOTween.To(() => value, x => value = x, endValue, tweenTime).SetEase(Ease.OutExpo)
                .OnUpdate(() =>
                {
                    unlockableDataSo.unlockableData.alreadyInvested = alreadyInvested + (initialCount - value);
                    
                    costText.SetText(ShortCount.Shorten(unlockableDataSo.unlockableData.unlockCost-unlockableDataSo.unlockableData.alreadyInvested));
                    resourceManagerSo.SetResource(value, unlockableDataSo.requiredResourceSo, false);
                }).
                OnComplete(OnCompleteScoreTween);
        }

        private void Expense()
        {
            ExpenseEffectManager.instance.Expense(unlockableDataSo.requiredResourceSo, ReferenceManager.instance.player.position + Vector3.up, costAreaObject.position);
        }

        private void StopExpense()
        {
            CancelInvoke(nameof(Expense));
        }

        private void OnCompleteScoreTween()
        {
            StopExpense();
            unlockableDataSo.unlockableData.isUnlocked = unlockableDataSo.unlockableData.alreadyInvested >=
                                                         unlockableDataSo.unlockableData.unlockCost;

            if (unlockableDataSo.unlockableData.isUnlocked)
            {
                NewlyUnlocked();
            }

            unlockableManagerSo.OnUpdateUnlockable();
            
            resourceManagerSo.FireStoreResource();
        }

        private void OnStoppedScoreTween()
        {
            StopExpense();
            unlockableManagerSo.OnUpdateUnlockable();
            resourceManagerSo.FireStoreResource();
        }

        [Button]
        private void NewlyUnlocked()
        {
            Unlock();
            HireWorkerOnUnlock();
            OnTaskComplete();
            unlockableDataSo.FireUnlocked();
            
            Invoke(nameof(ShowInteractionUI), 2.0f);
        }

        private void Unlock()
        {
            float mainObjectAppearTimeDelay = 1.2f;
            float poofAppearTimeDelay = 0.7f;

            newObjectCollider = mainObject.GetComponent<Collider>();
            
            OnUnlock();

            if (unlockableArea)
            {
                mainObjectAppearTimeDelay = 0.2f;
                poofAppearTimeDelay = 0.01f;
            }

            if (!unlockableArea)
            {
                underConstructionManager.StartUnlockAnim();
                Invoke(nameof(ShowPoof), poofAppearTimeDelay);
            }

            Invoke(nameof(AppearMainObject), mainObjectAppearTimeDelay);
        }

        protected abstract void HireWorkerOnUnlock();

        protected abstract void OnUnlock();

        
        [Button]
        private void AppearMainObject()
        {
            var appearTime = 0.5f;
            
            mainObject.localScale = Vector3.zero;
            
            mainObject.gameObject.SetActive(true);

            costAreaObject.DOScale(Vector3.zero, appearTime).OnComplete(OnHideCostArea);
            
            mainObject.DOScale(Vector3.one * 1.5f, appearTime/2.0f).SetEase(Ease.OutExpo).OnComplete(OnCompleteScaleUp);
            
            //mainObject.DOLocalJump(mainObject.localPosition, 0.2f,1, appearTime + 0.1f).SetEase(Ease.OutFlash);
        }

        private void ShowPoof()
        {
            poof.Play();
        }

        private void OnHideCostArea()
        {
            costAreaObject.gameObject.SetActive(false);
            costAreaObject.localScale = Vector3.one;
        }

        private void OnCompleteScaleUp()
        {
            mainObject.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce).OnComplete(OnCompleteAppearTween);
        }

        [Button]
        private void OnCompleteAppearTween()
        {
            //PathUpdater.Rescan(_newObjectCollider);
            ReferenceManager.instance.navmeshSurface.BuildNavMesh();
        }
        

        public CuttingToolSO GetInteractionTool()
        {
            return null;
        }
        
        protected override void TaskStarted()
        {
            
        }

        // public void OnTaskComplete()
        // {
        //     _onTaskCompleteCallback?.Invoke();
        //
        //     DisableVCam();
        // }
        //
        // public void SetAsNextTask(Action onTaskCompleteCallback, Action<int> onTaskProgressCallback)
        // {
        //     _onTaskCompleteCallback = onTaskCompleteCallback;
        //     _onTaskProgressCallback = onTaskProgressCallback;
        //     
        //     vCam.SetActive(true);
        //     
        //     Invoke(nameof(DisableVCam), 4.0f);
        // }
        //
        // private void DisableVCam()
        // {
        //     CancelInvoke(nameof(DisableVCam));
        //
        //     if (vCam)
        //     {
        //         vCam.SetActive(false);
        //     }
        // }
    }
}