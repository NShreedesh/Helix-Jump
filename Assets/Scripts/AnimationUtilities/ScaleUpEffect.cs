using System.Collections;
using UnityEngine;

namespace AnimationUtilities
{
    public class ScaleUpEffect : IAnimationEffect
    {
        private Transform ObjectTransform { get; }
        private Vector3 MaxSize { get; }
        private float ScaleSpeed { get; }

        public ScaleUpEffect(Transform objectTransform, Vector3 maxSize, float scaleSpeed)
        {
            ObjectTransform = objectTransform;
            MaxSize = maxSize;
            ScaleSpeed = scaleSpeed;
        }
        
        public IEnumerator Execute()
        {
            var time = 0f;
            var currentScale = ObjectTransform.localScale;

            while (ObjectTransform.localScale != MaxSize)
            {
                time += Time.deltaTime * ScaleSpeed;
                var scale = Vector3.Lerp(currentScale, MaxSize, time);
                ObjectTransform.localScale = scale;
                yield return null;
            }
        }
    }
}
