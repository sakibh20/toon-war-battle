using _Project.Core.Custom_Debug_Log.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using skb_sec._Project.Core.Pool_Manager.Script;
using skb_sec._Project.Scripts.Expense_Effect_System;
using UnityEngine;

namespace skb_sec._Project.Scripts.Pick_System
{
    public class AirDrop : Pickable
    {
        [SerializeField] private Pickable coinPrefab;
        
        protected override void PickUpEffect(Transform targetTransform)
        {
            //Destroy(targetObject);
            //CustomDebug.LogWarning("Handle pickup effect");

            InstantiateCoins();
            
            Invoke(nameof(OnCompleteAppearTween), 0.3f);
        }

        [Button]
        private void InstantiateCoins()
        {
            float range = 2.0f;
            
            //var targetPos = Vector3.zero;
            
            int coinCount = Value/2;
            Value = 0;

            for (int i = 0; i < coinCount; i++)
            {
                // targetPos.x = Random.Range(-range, range);
                // targetPos.z = Random.Range(-range, range);

                var coin = Instantiate(coinPrefab, transform);
                
                coin.transform.localPosition = Vector3.zero;
                
                coin.Value = 2;
                coin.destroyOnCollect = true;
                coin.InitPickable(PoolItemType.GoldPart, transform);
            }
        }
        
        private void OnCompleteAppearTween()
        {
            transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.Flash).OnComplete(OnCompleteCollect);
        }

        protected override void Appear()
        {
            
        }
    }
}
