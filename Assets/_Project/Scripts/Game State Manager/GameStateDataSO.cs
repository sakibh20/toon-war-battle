using UnityEngine;
using System;

namespace skb_sec._Project.Scripts.Game_State_Manager
{
    [CreateAssetMenu(menuName = "SKB Custom SO/Game Manager/Create Game Data Data", fileName = "Game State Data SO"), Serializable]
    public class GameStateDataSO : ScriptableObject
    {
        public GameState currentGameState;

        public event Action OnGameStateChanged;
        
        public void GameStart()
        {
            currentGameState = GameState.Running;
            
            FireOnGameStateChanged();
        }
        
        public void FireOnGameStateChanged()
        {
            OnGameStateChanged?.Invoke();
        }
    }
}
