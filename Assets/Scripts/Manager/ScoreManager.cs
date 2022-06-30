using System;
using Static;
using UnityEngine;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private int score;
        [SerializeField]
        private int highScore;

        public static Action<int> OnScoreUpdate;
        public static Action<int> OnHighScoreUpdate;

        private void Start()
        {
            LoadHighScore();
            OnScoreUpdate?.Invoke(score);
        }

        public void UpdateScore(int scoreIncrementValue)
        {
            score += scoreIncrementValue;
            OnScoreUpdate?.Invoke(score);
            SaveScore();
        }

        private void LoadHighScore()
        {
            highScore = PlayerPrefs.HasKey(SaveLoadTagManager.HighScoreKey)
                ? PlayerPrefs.GetInt(SaveLoadTagManager.HighScoreKey, highScore)
                : 0;
            OnHighScoreUpdate?.Invoke(highScore);
        }

        private void SaveScore()
        {
            if (score <= highScore) return;

            highScore = score;
            PlayerPrefs.SetInt(SaveLoadTagManager.HighScoreKey, highScore);
            OnHighScoreUpdate?.Invoke(highScore);
        }
    }
}
