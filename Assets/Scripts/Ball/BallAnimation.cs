using System;
using UnityEngine;

namespace Ball
{
    public class BallAnimation : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rb;
        
        [SerializeField] 
        private float scaleIncrement;
        [SerializeField] 
        private float minScale;
        [SerializeField] 
        private float maxScale;
        
        private void Update()
        {
            switch (rb.velocity.y)
            {
                case > 0:
                    ScaleUpAnimation();
                    break;
                case < 0:
                    ScaleDownAnimation();
                    break;
            }
        }

        private void ScaleUpAnimation()
        {
            var ballScale = transform.localScale;
            
            ballScale.x += scaleIncrement * Time.deltaTime;
            ballScale.y += scaleIncrement * Time.deltaTime;
            ballScale.z += scaleIncrement * Time.deltaTime;
            
            Scale(ballScale);
        }
        
        private void ScaleDownAnimation()
        {
            var ballScale = transform.localScale;
            
            ballScale.x -= scaleIncrement * Time.deltaTime;
            ballScale.y -= scaleIncrement * Time.deltaTime;
            ballScale.z -= scaleIncrement * Time.deltaTime;
            
            Scale(ballScale);
        }

        private void Scale(Vector3 ballScale)
        {
            ballScale.x = Math.Clamp(ballScale.x, minScale, maxScale);
            ballScale.y = Math.Clamp(ballScale.y, minScale, maxScale);
            ballScale.z = Math.Clamp(ballScale.z, minScale, maxScale);
            
            transform.localScale = ballScale;
        }
    }
}
