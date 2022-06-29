using AnimationUtilities;
using UnityEngine;

namespace ShapeScripts.Animation
{
    public class ScaleAnimation : MonoBehaviour
    {
        [Header("Other Components")] 
        [SerializeField]
        private ShapeAnimation shapeAnimation;

        [Header("Scale Animation Effect")] 
        [SerializeField]
        private Vector3 minScale = new Vector3(0.3f, 0.3f, 0.3f);
        [SerializeField]
        private Vector3 maxScale = new Vector3(0.5f, 0.5f, 0.5f);
        [SerializeField] 
        private float scaleSpeed = 2;

        public void Awake()
        {
            shapeAnimation.GoingUpAnimationEffects.Add(new ScaleUpEffect(transform, maxScale, scaleSpeed));
            shapeAnimation.GoingDownAnimationEffects.Add(new ScaleDownEffect(transform, minScale, scaleSpeed));
        }
    }
}
