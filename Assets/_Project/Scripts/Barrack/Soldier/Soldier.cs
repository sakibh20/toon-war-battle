using System;
using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Interactable.Damageable;
using UnityEngine;
using UnityEngine.AI;

namespace skb_sec._Project.Scripts.Barrack.Soldier
{
    public abstract class Soldier : MonoBehaviour
    {
        [SerializeField, Required] private SoldierManagerSO soldierManagerSo;

        [SerializeField, Required] protected NavMeshAgent agent;

        [SerializeField, Required] protected SoldierAnimatorManager soldierAnimatorManager;
        
        [SerializeField, Required] protected SoldierDataSO soldierData;
        
        private int _maxHealth;

        [ShowInInspector, ReadOnly] private Damageable _targetDamageable;

        private bool _navigating;
        private bool _attacking;

        public Vector3 initialPosition;

        private void Awake()
        {
            initialPosition = transform.position;
        }

        protected virtual void Start()
        {
            _maxHealth = soldierData.health;
            
            //soldierAnimatorManager.IdleAnimation();
            soldierManagerSo.Enlist(this);
        }
        
        private void OnReachedEnd()
        {
            _navigating = false;
            
            if (_attacking)
            {
                _attacking = false;
                soldierAnimatorManager.AttackAnimation();

                LookAtTarget();
            }
            else
            {
                soldierAnimatorManager.IdleAnimation();
            }
        }

        private void LookAtTarget()
        {
            var direction = _targetDamageable.transform.position - transform.position;
            direction.y = 0;

            var targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.DORotate(targetRotation.eulerAngles, 0.5f);
        }

        private void OnDead()
        {
            soldierManagerSo.DeList(this);
            if (_targetDamageable)
            {
                _targetDamageable.DeListInAttackingSoldierList(this);   
            }

            CustomDebug.LogWarning("Better Handle Dead");
            
            Destroy(gameObject);
        }
        
        private void Update()
        {
            if(NavMesh.SamplePosition(transform.position, out var myNavHit, 100 , -1))
            {
                Vector3 targetPos = myNavHit.position;
                targetPos.y = transform.position.y;
                
                transform.position = targetPos;
            }

            
            if (!agent.pathPending && _navigating)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        //CustomDebug.Log("Destination Reached");
                        
                        OnReachedEnd();
                    }
                }
            }
        }

        
        public void TakeDamage(int damage)
        {
            _maxHealth -= damage;
            
            if (_maxHealth<=0)
            {
                OnDead();
            }
        }

        public void Attack(Damageable target)
        {
            _targetDamageable = target;
            
            var destination = _targetDamageable.transform.position;
            destination.y = transform.position.y;
            
            _attacking = true;

            RunToDestination(destination);
            // agent.destination = destination;
            // _navigating = true;
            // _attacking = true;
            //
            // agent.speed = soldierData.runSpeed;
            //
            // soldierAnimatorManager.RunAnimation();
        }

        private void RunToDestination(Vector3 position)
        {
            agent.destination = position;
            _navigating = true;
            //_attacking = true;

            agent.speed = soldierData.runSpeed;
            
            soldierAnimatorManager.RunAnimation();
        }

        public void HitOnTarget()
        {
            if (_targetDamageable)
            {
                _targetDamageable.TakeDamage(soldierData.damagePower);
            }
        }

        public void ClearDamageable()
        {
            _targetDamageable = null;
        }

        public Damageable CurrentDamageable()
        {
            return _targetDamageable;
        }

        public void MoveBackToInitialPosition()
        {
            _attacking = false;
            RunToDestination(initialPosition);
        }

        public void SetDestination(Vector3 position)
        {
            //initialPosition = position;
            
            var destination = position;
            position.y = transform.position.y;
            
            agent.destination = destination;
            
            _attacking = false;
            _navigating = true;
            
            agent.speed = soldierData.walkSpeed;
            soldierAnimatorManager.MarchAnimation();
        }
    }
}
