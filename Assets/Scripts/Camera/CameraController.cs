using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Follow Info")] 
        [SerializeField]
        private Transform targetTransform;
        [SerializeField] 
        private Vector3 offset;

        private void Start()
        {
            transform.position = offset;
        }

        private void LateUpdate()
        {
            if(targetTransform.Equals(null)) return;
            if(targetTransform.position.y > transform.position.y - offset.y) return;
            
            var targetPosition = targetTransform.position + offset;
            targetPosition.z = offset.z;
            transform.position = targetPosition;
        }

        public void AssignTarget(Transform target)
        {
            targetTransform = target;
        }
    }
}
