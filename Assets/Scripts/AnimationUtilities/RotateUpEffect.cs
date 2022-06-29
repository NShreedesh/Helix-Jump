using System.Collections;
using UnityEngine;

namespace AnimationUtilities
{
    public class RotateUpEffect : IAnimationEffect
    {
        private Transform ObjectTransform { get; }
        private Vector3 MaxRotation { get; }
        private float RotationSpeed { get; }
        
        public RotateUpEffect(Transform objectTransform, Vector3 maxRotation, float rotationSpeed)
        {
            ObjectTransform = objectTransform;
            MaxRotation = maxRotation;
            RotationSpeed = rotationSpeed;
        }
        
        public IEnumerator Execute()
        {
            var currentRotation = ObjectTransform.eulerAngles;
            var time = 0f;
            
            while (ObjectTransform.eulerAngles != MaxRotation)
            {
                time += Time.deltaTime * RotationSpeed;
                var rotation = Vector3.Lerp(currentRotation, MaxRotation, time);
                ObjectTransform.eulerAngles = rotation;
                yield return null;
            }
        }
    }
}
