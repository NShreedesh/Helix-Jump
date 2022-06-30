using System;
using Static;
using UnityEngine;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Scores")]
        [SerializeField]
        private int score;
        [SerializeField]
        private int highScore;
        public int StoredScore { get; private set; }

        [Header("Actions")]
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
            StoredScore += scoreIncrementValue;
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

        public void ResetStoredScore()
        {
            StoredScore = 0;
        }
    }
}
