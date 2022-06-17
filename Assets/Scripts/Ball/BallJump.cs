using Audio;
using Static;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ball
{
    public class BallJump : MonoBehaviour
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
        private GameObject[] splashSprites;

        [Header("Audio Manager Component")] 
        [HideInInspector]
        public AudioManager audioManager;
        
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip ballCollideAudioClip;
        [SerializeField] private AudioClip pointScoredAudioClip;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.HelixNonKill))
            {
                if(!_canJump) return;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _canJump = false;
                
                audioManager.PlayOneShotAudio(ballCollideAudioClip);
            
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
                print("Point Scored");
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
