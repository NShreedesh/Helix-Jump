using System;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private State gameState;
        public State GameState
        {
            get => gameState;
            private set
            {
                gameState = value;
                ChangeGameState(gameState);
            }
        }

        public Action GameIdleAction;
        public Action GamePlayingAction;
        public Action GameLoseAction;
        public Action GameWinAction;

        private void Awake()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    Application.targetFrameRate = 30;
                    break;
            }
        }

        private void Start()
        {
            gameState = State.Idle;
        }

        private void OnValidate()
        {
            ChangeGameState(gameState);
        }

        public void ChangeGameState(State state)
        {
            this.gameState = state;

            switch (gameState)
            {
                case State.Idle:
                    IdleAction();
                    break;
                case State.Playing:
                    PlayingAction();
                    break;
                case State.Win:
                    WinAction();
                    break;
                case State.Lose:
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
        
        public enum State
        {
            Idle,
            Playing,
            Win,
            Lose,
        }
    }
}
