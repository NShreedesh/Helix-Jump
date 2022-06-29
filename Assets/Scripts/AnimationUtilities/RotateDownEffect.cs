using System.Collections;
using UnityEngine;

namespace AnimationUtilities
{
    public class RotateDownEffect : IAnimationEffect
    {
        private Transform ObjectTransform { get; }
        private Vector3 MinRotation { get; }
        private float RotationSpeed { get; }
        
        public RotateDownEffect(Transform objectTransform, Vector3 minRotation, float rotationSpeed)
        {
            ObjectTransform = objectTransform;
            RotationSpeed = rotationSpeed;
            MinRotation = minRotation;
        }
        
        public IEnumerator Execute()
        {
            var currentRotation = ObjectTransform.eulerAngles;
            var time = 0f;
            
            while (ObjectTransform.eulerAngles != MinRotation)
            {
                time += Time.deltaTime * RotationSpeed;
                var rotation = Vector3.Lerp(currentRotation, MinRotation, time);
                ObjectTransform.eulerAngles = rotation;
                yield return null;
            }
        }
    }
}
