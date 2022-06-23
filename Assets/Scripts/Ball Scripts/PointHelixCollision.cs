using Cylinder_Scripts;
using Static;
using UnityEngine;

namespace Ball_Scripts
{
    public class PointHelixCollision : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField]
        private BallSetup ballSetup;
        
        [Header("Audio Clips")]
        [SerializeField] 
        private AudioClip pointScoredAudioClip;

        [Header("Score Values")]
        [SerializeField]
        private int scoreIncrementValue = 10;
       
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(TagManager.HelixPoint)) return;
            
            ballSetup.AudioManager.PlayOneShotAudio(pointScoredAudioClip);
            ballSetup.ScoreManager.UpdateScore(scoreIncrementValue);

            if(!other.transform.parent.TryGetComponent<Cylinder>(out var cylinder)) return;
            cylinder.DamageIfPointIsScored();
        }
    }
}
