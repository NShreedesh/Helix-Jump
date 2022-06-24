using Static;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private int score;
        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField] 
        private int highScore;
        [SerializeField]
        private TMP_Text highScoreText;

        private void Start()
        {
            LoadHighScore();
            ChangeScoreUI(scoreText, score);
        }

        public void UpdateScore(int scoreIncrementValue)
        {
            score += scoreIncrementValue;
            ChangeScoreUI(scoreText, score);
            SaveScore();
        }

        private void LoadHighScore()
        {
            highScore = PlayerPrefs.HasKey(SaveLoadTagManager.HighScoreKey)
                ? PlayerPrefs.GetInt(SaveLoadTagManager.HighScoreKey, highScore)
                : 0;
            ChangeScoreUI(highScoreText, highScore);
        }

        private void SaveScore()
        {
            if (score <= highScore) return;

            highScore = score;
            PlayerPrefs.SetInt(SaveLoadTagManager.HighScoreKey, highScore);
            ChangeScoreUI(highScoreText, highScore);
        }
        
        private void ChangeScoreUI(TMP_Text whichScoreText, int scoreValue)
        {
            whichScoreText.text = scoreValue.ToString();
        }
    }
}
