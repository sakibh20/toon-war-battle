using System;
using Sirenix.OdinInspector;
using skb_sec._Project.Scripts.Dialogue_System;
using skb_sec._Project.Scripts.Task_Manager.Xp;
using UnityEngine;

namespace skb_sec._Project.Scripts.Game_State_Manager
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField, Required] private XpDataManagerSO xpDataManagerSo;
        [SerializeField, Required] private DialogueDataSO dialogueDataSo;
        [SerializeField, Required] private GameStateDataSO gameStateDataSo;
        
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
            xpDataManagerSo.OnLevelDataLoaded += InitState;
        }

        private void UnSubscribe()
        {
            xpDataManagerSo.OnLevelDataLoaded -= InitState;
        }
        
        private void InitState()
        {
            if (!dialogueDataSo.showed)
            {
                dialogueDataSo.OnDialogueComplete += InitGameStart;
            }
            else
            {
                Invoke(nameof(GameStart), 1.0f);
            }
        }

        private void InitGameStart()
        {
            Invoke(nameof(GameStart), 4.0f);
        }

        private void GameStart()
        {
            gameStateDataSo.GameStart();
        }
    }

    public enum GameState
    {
        None,
        Running,
        Paused,
        End
    }
}
