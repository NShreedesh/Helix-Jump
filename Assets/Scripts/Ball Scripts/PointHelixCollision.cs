using Player;
using Static;
using UnityEngine;

namespace Ball_Scripts
{
    public class PointHelixCollision : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField]
        private Ball ball;
        
        [Header("Audio Clips")]
        [SerializeField] 
        private AudioClip pointScoredAudioClip;

        [Header("Score Values")]
        [SerializeField]
        private int scoreIncrementValue = 10;
       
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(TagManager.HelixPoint)) return;
            
            ball.AudioManager.PlayOneShotAudio(pointScoredAudioClip);
            ball.ScoreManager.UpdateScore(scoreIncrementValue);

            var rotateHelix = other.GetComponentInParent<RotateHelix>();
            if (rotateHelix == null) return;

            for (var i = 0; i < rotateHelix.transform.childCount; i++)
            {
                Destroy(rotateHelix.transform.GetChild(i).gameObject);
            }
        }
    }
}
