using Audio;
using Score;
using Static;
using UnityEngine;

namespace Ball_Scripts
{
    public class Ball : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] 
        private Rigidbody rb;

        [Header("Ball Jump Values")]
        [SerializeField] 
        private float jumpForce;
        private bool _canJump = true;

        [Header("Splash Effect")] 
        [SerializeField]
        private ParticleSystem[] splashParticles;

        [Header("Audio Manager Component")] 
        [HideInInspector]
        public AudioManager audioManager;
        
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip ballCollideAudioClip;
        [SerializeField] private AudioClip pointScoredAudioClip;

        [SerializeField] 
        private ScoreManager scoreManager;
        [SerializeField]
        private int scoreIncrementValue = 10;

        private void Start()
        {
            scoreManager = GameObject.FindGameObjectWithTag(TagManager.ScoreManager).GetComponent<ScoreManager>();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.HelixNonKill))
            {
                if(!_canJump) return;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _canJump = false;

                var randomSplashEffect = Random.Range(0, splashParticles.Length);
                
                audioManager.PlayOneShotAudio(ballCollideAudioClip);

                var v = collision.GetContact(0).point;
                v.y += collision.collider.bounds.extents.y;
                var spawnedSplashParticle = Instantiate(splashParticles[randomSplashEffect], v, splashParticles[randomSplashEffect].transform.rotation, collision.transform);
                spawnedSplashParticle.Play();
            
                Invoke(nameof(CheckJump), 0.2f);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixKill))
            {
                print("Dead");
                audioManager.PlayOneShotAudio(ballCollideAudioClip);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixLevelComplete))
            {
                print("Level Completed");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TagManager.HelixPoint))
            {
                audioManager.PlayOneShotAudio(pointScoredAudioClip);
                scoreManager.UpdateScore(scoreIncrementValue);
            }
        }

        private void CheckJump()
        {
            _canJump = true;
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}
