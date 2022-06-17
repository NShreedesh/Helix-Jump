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
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.HelixNonKill))
            {
                if(!_canJump) return;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _canJump = false;
            
                Invoke(nameof(CheckJump), 0.2f);
            }
            
            else if (collision.gameObject.CompareTag(TagManager.HelixKill))
            {
                print("Dead");
                SceneManager.LoadScene(0);
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
