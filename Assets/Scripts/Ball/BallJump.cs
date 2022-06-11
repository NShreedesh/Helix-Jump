using System;
using UnityEngine;

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
            if(!_canJump) return;
            
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _canJump = false;
            
            Invoke(nameof(CheckJump), 0.2f);
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
