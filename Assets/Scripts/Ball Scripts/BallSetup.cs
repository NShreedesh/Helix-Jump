using Manager;
using UnityEngine;

namespace Ball_Scripts
{
    public class BallSetup : MonoBehaviour
    {
        [field: Header("Manager Components")] 
        public AudioManager AudioManager { get; private set; }
        public ScoreManager ScoreManager { get; private set; }
        public GameManager GameManager { get; private set; }
        public Color32 SplashColor { get; private set; }

        public void SetAudioManager(AudioManager audioManager)
        {
            AudioManager = audioManager;
        }
        
        public void SetScoreManager(ScoreManager scoreManager)
        {
            ScoreManager = scoreManager;
        }
        
        public void SetGameManager(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        
        public void SetSplashColor(Color32 color)
        {
            color.a = 255;
            SplashColor = color;
        }
    }
}
