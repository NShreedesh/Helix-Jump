using ShapeScripts;
using Manager;
using Static;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShapeScripts
{
    public class HelixCollision : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] 
        private Rigidbody rb;
        [SerializeField] 
        private ShapeSetup shapeSetup;

        [Header("Ball Jump Values")]
        [SerializeField] 
        private float jumpForce = 7;
        private bool _canJump = true;
        
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip ballCollideAudioClip;
        [SerializeField] private AudioClip deadAudioClip;
        
        [Header("Splash Effect")] 
        [SerializeField]
        private GameObject[] splashSprites;

        private void OnCollisionEnter(Collision collision)
        {
            if(shapeSetup.GameManager.GameState != GameManager.State.Playing) return;
            
            if (collision.gameObject.CompareTag(TagManager.HelixKill))
            {
                Die();
                shapeSetup.AudioManager.PlayOneShotAudio(deadAudioClip);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixNonKill))
            {
                if(!_canJump) return;
                
                Jump();
                Invoke(nameof(CheckJump), 0.2f);
                SplashEffect(collision);
                shapeSetup.AudioManager.PlayOneShotAudio(ballCollideAudioClip);
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
            var randomSplashEffect = Random.Range(0, splashSprites.Length);

            var splashEffectSpawnPosition = collision.GetContact(0).point;
            splashEffectSpawnPosition.y = collision.transform.position.y;
            splashEffectSpawnPosition.y += collision.collider.bounds.size.y + 0.01f;

            var randomZRotation = Random.Range(0, 360);
            var splashEffectRotation = splashSprites[randomSplashEffect].transform.rotation;
            splashEffectRotation.z = randomZRotation;
            
            var spawnedSplash = Instantiate(splashSprites[randomSplashEffect], collision.transform);
            spawnedSplash.transform.position = splashEffectSpawnPosition;
            spawnedSplash.transform.localRotation = splashEffectRotation;
            spawnedSplash.GetComponent<SpriteRenderer>().color = shapeSetup.SplashColor;
            Destroy(spawnedSplash, 3);
        }

        private void Die()
        {
            rb.isKinematic = true;
            StopBallJump();
            shapeSetup.GameManager.ChangeGameState(GameManager.State.Lose);
        }
        
        private void LevelComplete()
        {
            StopBallJump();
            shapeSetup.GameManager.ChangeGameState(GameManager.State.Win);
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