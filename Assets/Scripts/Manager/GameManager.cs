using System;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameState gameState;

        public Action GameIdleAction;
        public Action GamePlayingAction;
        public Action GameLoseAction;
        public Action GameWinAction;

        private void Start()
        {
            gameState = GameState.Idle;
        }

        public void ChangeGameState(GameState state)
        {
            gameState = state;

            switch (gameState)
            {
                case GameState.Idle:
                    IdleAction();
                    break;
                case GameState.Playing:
                    PlayingAction();
                    break;
                case GameState.Win:
                    WinAction();
                    break;
                case GameState.Lose:
                    LoseAction();
                    break;
                default:
                    break;
            }
        }

        private void IdleAction()
        {
            GameIdleAction?.Invoke();
        }

        private void PlayingAction()
        {
            GamePlayingAction?.Invoke();
        }

        private void WinAction()
        {
            GameWinAction?.Invoke();
        }

        private void LoseAction()
        {
            GameLoseAction?.Invoke();
        }
        
        public enum GameState
        {
            Idle,
            Playing,
            Win,
            Lose,
        }
    }
}
