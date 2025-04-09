using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Pathfinding;
using Pathfinding.Examples;
using Pathfinding.RVO;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Character;
using skb_sec._Project.Scripts.Economy;
using skb_sec._Project.Scripts.Expense_Effect_System;
using skb_sec._Project.Scripts.Interactable.Cut_System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace skb_sec._Project.Scripts.Hireables
{
    public abstract class Hireables : MonoBehaviour
    {
        [SerializeField] private HireableUiManager hireableUiManager;
        [SerializeField] private AnimatorManager animatorManager;
        [SerializeField] private ResourceManagerSO resourceManagerSo;
        [SerializeField] private WorkerDataManagerSO workerDataManagerSo;

        [SerializeField] private AlICutableSO alICutableSo;
        [SerializeField] private WorkerDataSO workerData;
        
        [SerializeField] private Storage.Storage storage;
        //[SerializeField] private AIPath aiPath;
        
        [SerializeField] private NavMeshAgent agent;
        //[SerializeField] private RVOController rvoController;
        //[SerializeField] private RVOExampleAgent rvoAgent;

        private Cutables _targetCutable;

        private bool _isAtDestination;
        private bool _setForStorage;

        private bool _navigating;

        private void Awake()
        {
            if (workerDataManagerSo.dataLoaded)
            {
                CheckHire();
            }
            else
            {
                workerDataManagerSo.WorkerDataLoaded += CheckHire;
            }
        }

        private void OnDisable()
        {
            workerDataManagerSo.WorkerDataLoaded -= CheckHire;
        }

        private void CheckHire()
        {
            if (workerData.workerAllLevelsSo.hired)
            {
                CustomDebug.Log("Already Hired");
                StartJob();
            }
        }

        [Button]
        public void Hire()
        {
            if (workerData.workerAllLevelsSo.hired)
            {
                CustomDebug.Log("Already Hired");
                StartJob();
                return;
            }
            
            if (workerData.hiringResourceSo.Value < workerData.workerAllLevelsSo.currentLevel)
            {
                CustomDebug.LogError("Not Enough Resource Hire");
                return;
            }
            
            CustomDebug.Log("Newly Hired");
            
            resourceManagerSo.DecreaseResource(workerData.workerAllLevelsSo.currentLevel, workerData.hiringResourceSo, true);
            workerData.workerAllLevelsSo.hired = true;
            workerDataManagerSo.FireWorkerDataUpdated();
            StartJob();
        }

        private void StartJob()
        {
            if (workerData.workerAllLevelsSo.storedValue == workerData.workerAllLevelsSo.allUpgrades[workerData.workerAllLevelsSo.currentLevel].maxCapacity)
            {
                GoToStorage();
            }
            else
            {
                SetDestination();
            }
        }

        [Button]
        private void SetDestination()
        {
            _targetCutable = alICutableSo.GetNearestItem(workerData.storingResourceSo, transform);

            if (_targetCutable == null)
            {
                CustomDebug.LogWarning("No Current Cutable");
                RetrySetDestination();
                return;
            }
            
            CancelInvoke(nameof(RetrySetDestination));

            // aiPath.destination = _targetCutable.transform.position;
            // aiPath.canMove = true;

            _navigating = true;
            agent.destination = _targetCutable.transform.position;
            
            animatorManager.RunAnimation();
        }

        private void RetrySetDestination()
        {
            Invoke(nameof(SetDestination), 2.0f);
        }

        // protected void Update() {
        //     if (aiPath.reachedEndOfPath)
        //     {
        //          if (!_isAtDestination)
        //          {
        //              OnTargetReached();
        //          }
        //          _isAtDestination = true;
        //     }
        //     else
        //     {
        //          _isAtDestination = false;
        //     }
        // }

        private void OnTargetReached()
        {
            //CustomDebug.Log("Reached Target Pos");

            Action();
            
            _setForStorage = false;
        }

        private void Action()
        {
            if (!_setForStorage)
            {
                LookAtTarget();
            }
            else
            {
                //aiPath.canMove = false;

                _navigating = false;
                
                Unload();
            }
        }

        
        private void LookAtTarget()
        {
            Vector3 targetAngle = Quaternion.LookRotation(_targetCutable.transform.position - transform.position).eulerAngles;
            targetAngle.x = 0;
            targetAngle.z = 0;

            transform.DORotate(targetAngle, 0.4f).OnComplete(OnCompleteLookAt);
        }

        private void OnCompleteLookAt()
        {
            Cut();
        }

        private void Cut()
        {
            animatorManager.CuttingAnimation();
        }

        public void HitCutable()
        {
            if (_targetCutable.CurrentHealth > 0)
            {
                _targetCutable.StartInteraction();

                if (_targetCutable.CurrentHealth == 0)
                {
                    animatorManager.IdleAnimation();
                    SetDestination();
                }
            }
            else
            {
                SetDestination();
            }
        }

        private void GoToStorage()
        {
            _setForStorage = true;
            
            // aiPath.destination = storage.transform.position;
            // aiPath.canMove = true;

            _navigating = true;
            agent.destination = storage.transform.position;
            
            animatorManager.RunAnimation();
        }

        private void Unload()
        {
            animatorManager.IdleAnimation();
            StartStoreInStorage();
        }
        
        public void AddResource()
        {
            if(!workerData.workerAllLevelsSo.hired)
            {
                CustomDebug.LogError("Hire First");
                return;
            }
            
            if (workerData.workerAllLevelsSo.storedValue < workerData.workerAllLevelsSo.allUpgrades[workerData.workerAllLevelsSo.currentLevel].maxCapacity)
            {
                workerData.workerAllLevelsSo.storedValue += 1;
                hireableUiManager.UpdateInfo();
            }
            else
            {
                GoToStorage();
            }
        }


        [Button]
        public void StartStoreInStorage()
        {
            if (!workerData.storageUnlockableDataSo.unlockableData.isUnlocked)
            {
                CustomDebug.LogError("Unlock The storage first");
                Invoke(nameof(StartStoreInStorage), 2.0f);
                return;
            }
            
            if (storage.Storable(workerData.storingResourceSo))
            {
                CancelInvoke(nameof(StartStoreInStorage));
                InvokeRepeating(nameof(StoreRepeating), 0.1f, 0.1f);
            }
            else
            {
                Invoke(nameof(StartStoreInStorage), 2.0f);
            }
        }
        
        
        private void StoreRepeating()
        {
            if (storage.Storable(workerData.storingResourceSo))
            {
                if(workerData.workerAllLevelsSo.storedValue == 0)
                {
                    StopStoring();
                    return;
                }
        
                workerData.workerAllLevelsSo.storedValue -= 1;
                storage.StoreOne(workerData.storingResourceSo);
                hireableUiManager.UpdateInfo();
                ExpenseEffectManager.instance.Expense(workerData.storingResourceSo, transform.position + Vector3.up, storage.transform.position);
            }
            else
            {
                StopStoring();
            }
        }
        
        private void StopStoring()
        {
            CancelInvoke(nameof(StoreRepeating));
            resourceManagerSo.FireStoreResource();
            
            SetDestination();
        }

        private void Update()
        {
            if(!_navigating) return;
            
            if(NavMesh.SamplePosition(transform.position, out var myNavHit, 100 , -1))
            {
                transform.position = myNavHit.position;
            }
            
            
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        //CustomDebug.Log("Reached EOP");
                        _navigating = false;
                        
                        if (!_isAtDestination)
                        {
                            OnTargetReached();
                        }
                        _isAtDestination = true;
                    }
                    else
                    {
                        _isAtDestination = false;
                    }
                }
                else
                {
                    _isAtDestination = false;
                }
            }
            else
            {
                _isAtDestination = false;
            }
            
            
            // if (aiPath.reachedEndOfPath)
            // {
            //     if (!_isAtDestination)
            //     {
            //         OnTargetReached();
            //     }
            //     _isAtDestination = true;
            // }
            // else
            // {
            //     _isAtDestination = false;
            // }
        }
    }
}
