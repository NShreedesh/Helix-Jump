using Static;
using UnityEngine;

namespace Ball_Scripts
{
    public class HelixCollision : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] 
        private Rigidbody rb;
        [SerializeField] 
        private Ball ball;

        [Header("Ball Jump Values")]
        [SerializeField] 
        private float jumpForce = 7;
        private bool _canJump = true;
        
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip ballCollideAudioClip;
        
        [Header("Splash Effect")] 
        [SerializeField]
        private ParticleSystem[] splashParticles;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.HelixNonKill))
            {
                if(!_canJump) return;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _canJump = false;

                var randomSplashEffect = Random.Range(0, splashParticles.Length);
                
                ball.AudioManager.PlayOneShotAudio(ballCollideAudioClip);

                var v = collision.GetContact(0).point;
                v.y += collision.collider.bounds.extents.y;
                var spawnedSplashParticle = Instantiate(splashParticles[randomSplashEffect], v, splashParticles[randomSplashEffect].transform.rotation, collision.transform);
                spawnedSplashParticle.Play();
            
                Invoke(nameof(CheckJump), 0.2f);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixKill))
            {
                print("Dead");
                ball.AudioManager.PlayOneShotAudio(ballCollideAudioClip);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixLevelComplete))
            {
                print("Level Completed");
            }
        }
        
        private void CheckJump() => _canJump = true;
        
        private void OnDisable() => CancelInvoke();
    }
}
