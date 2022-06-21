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
                
                Jump(collision);
                SplashEffect(collision);
                ball.AudioManager.PlayOneShotAudio(ballCollideAudioClip);

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

        private void Jump(Collision collision)
        {
            print(collision.GetContact(0).normal);
            
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _canJump = false;
        }

        private void SplashEffect(Collision collision)
        {
            var randomSplashEffect = Random.Range(0, splashParticles.Length);

            var v = collision.GetContact(0).point;
            v.y += collision.collider.bounds.extents.y;
            var spawnedSplashParticle = Instantiate(splashParticles[randomSplashEffect], v, splashParticles[randomSplashEffect].transform.rotation, collision.transform);
            
            var main = spawnedSplashParticle.main;
            main.startColor = new ParticleSystem.MinMaxGradient(ball.SplashColor);
            
            spawnedSplashParticle.Play();
        }
        
        private void CheckJump() => _canJump = true;
        
        private void OnDisable() => CancelInvoke();
    }
}
