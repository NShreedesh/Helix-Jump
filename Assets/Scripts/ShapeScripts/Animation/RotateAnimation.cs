using AnimationUtilities;
using UnityEngine;

namespace ShapeScripts.Animation
{
    public class RotateAnimation : MonoBehaviour
    {
        [Header("Other Components")] 
        [SerializeField]
        private ShapeAnimation shapeAnimation;
        
        [Header("Rotation Animation Effect")] 
        [SerializeField]
        private Vector3 minRotation = new Vector3(0, 0, 0);
        [SerializeField]
        private Vector3 maxRotation = new Vector3(0, 0, 180);
        [SerializeField]
        private float rotateSpeed = 2;

        public void Awake()
        {
            shapeAnimation.GoingUpAnimationEffects.Add(new RotateUpEffect(transform, maxRotation, rotateSpeed));
            shapeAnimation.GoingDownAnimationEffects.Add(new RotateDownEffect(transform, minRotation, rotateSpeed));
        }
    }
}
