using Manager;
using Static;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ball_Scripts
{
    public class HelixCollision : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] 
        private Rigidbody rb;
        [SerializeField] 
        private BallSetup ballSetup;

        [Header("Ball Jump Values")]
        [SerializeField] 
        private float jumpForce = 7;
        private bool _canJump = true;
        
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip ballCollideAudioClip;
        [SerializeField] private AudioClip deadAudioClip;
        
        [Header("Splash Effect")] 
        [SerializeField]
        private ParticleSystem[] splashParticles;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.HelixNonKill))
            {
                if(!_canJump) return;
                if(ballSetup.GameManager.GameState != GameManager.State.Playing) return;
                
                Jump();
                SplashEffect(collision);
                ballSetup.AudioManager.PlayOneShotAudio(ballCollideAudioClip);
            
                Invoke(nameof(CheckJump), 0.2f);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixKill))
            {
                Die();
                ballSetup.AudioManager.PlayOneShotAudio(deadAudioClip);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixLevelComplete))
            {
                LevelComplete();
            }
        }
        
        private void Jump()
        {
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
            main.startColor = new ParticleSystem.MinMaxGradient(ballSetup.SplashColor);
            
            spawnedSplashParticle.Play();
        }

        private void Die()
        {
            StopBallJump();
            ballSetup.GameManager.ChangeGameState(GameManager.State.Lose);
        }
        
        private void LevelComplete()
        {
            StopBallJump();
            ballSetup.GameManager.ChangeGameState(GameManager.State.Win);
        }
        
        private void CheckJump() => _canJump = true;

        private void StopBallJump()
        {
            rb.velocity = Vector3.zero;
            jumpForce = 0;
        }
        
        private void OnDisable() => CancelInvoke();
    }
}
