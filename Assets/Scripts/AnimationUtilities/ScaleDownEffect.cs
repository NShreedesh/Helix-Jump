using System.Collections;
using UnityEngine;

namespace AnimationUtilities
{
    public class ScaleDownEffect : IAnimationEffect
    {
        private Transform ObjectTransform { get; }
        private Vector3 MinSize { get; }
        private float ScaleSpeed { get; }

        public ScaleDownEffect(Transform objectTransform, Vector3 minSize, float scaleSpeed)
        {
            ObjectTransform = objectTransform;
            MinSize = minSize;
            ScaleSpeed = scaleSpeed;
        }
        
        public IEnumerator Execute()
        {
            var currentScale = ObjectTransform.localScale;
            var time = 0f;
            
            while (ObjectTransform.localScale != MinSize)
            {
                time += Time.deltaTime * ScaleSpeed;
                var scale = Vector3.Lerp(currentScale, MinSize, time);
                ObjectTransform.localScale = scale;
                yield return null;
            }
        }
    }
}
