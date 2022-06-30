using Cylinder_Scripts;
using Static;
using UnityEngine;

namespace ShapeScripts
{
    public class PointHelixCollision : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField]
        private ShapeSetup shapeSetup;
        
        [Header("Audio Clips")]
        [SerializeField] 
        private AudioClip pointScoredAudioClip;

        [Header("Score Values")]
        [SerializeField]
        private int scoreIncrementValue = 10;
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TagManager.HelixPoint))
            {
                shapeSetup.AudioManager.PlayOneShotAudio(pointScoredAudioClip);
                shapeSetup.ScoreManager.UpdateScore(scoreIncrementValue);

                DamageHelixDuringTrigger(other);
            }
            else if (other.gameObject.CompareTag(TagManager.HelixWithoutPoint))
            {
                DamageHelixDuringTrigger(other);
            }
        }

        private static void DamageHelixDuringTrigger(Collider other)
        {
            if (!other.transform.parent.TryGetComponent<HelixBlast>(out var cylinder)) return;
            cylinder.DamageHelix();
        }
    }
}
