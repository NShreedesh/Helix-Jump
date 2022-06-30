using Manager;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text highScoreText;
        [SerializeField]
        private TMP_Text scoreText;

        private void Start()
        {
            ScoreManager.OnScoreUpdate += ChangeScore;
            ScoreManager.OnHighScoreUpdate += ChangeHighScore;
        }

        private void ChangeScore(int score)
        {
            scoreText.text = score.ToString();
        }
        
        private void ChangeHighScore(int highScore)
        {
            highScoreText.text = highScore.ToString();
        }

        private void OnDisable()
        {
            ScoreManager.OnScoreUpdate -= ChangeScore;
            ScoreManager.OnHighScoreUpdate -= ChangeHighScore;
        }
    }
}
