using Manager;
using Score;
using UnityEngine;

namespace Ball_Scripts
{
    public class Ball : MonoBehaviour
    {
        [field: Header("Manager Components")] 
        public AudioManager AudioManager { get; private set; }
        public ScoreManager ScoreManager { get; private set; }
        public Color32 SplashColor { get; private set; }

        public void SetAudioManager(AudioManager audioManager)
        {
            AudioManager = audioManager;
        }
        
        public void SetScoreManager(ScoreManager scoreManager)
        {
            ScoreManager = scoreManager;
        }
        
        public void SetSplashColor(Color32 color)
        {
            color.a = 255;
            SplashColor = color;
        }
    }
}
