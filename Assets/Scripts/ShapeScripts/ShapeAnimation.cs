using System.Collections.Generic;
using AnimationUtilities;
using Manager;
using UnityEngine;

namespace ShapeScripts
{
    public class ShapeAnimation : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private ShapeSetup shapeSetup;

        [Header("Animation Effect")] 
        private readonly List<IAnimationEffect> _goingUpAnimationEffects = new();
        private readonly List<IAnimationEffect> _goingDownAnimationEffects = new();

        [Header("Scale Animation Effect")] 
        [SerializeField]
        private Vector3 minScale;
        [SerializeField]
        private Vector3 maxScale;
        [SerializeField] 
        private float scaleSpeed;
        
        [Header("Rotation Animation Effect")] 
        [SerializeField]
        private Vector3 minRotation;
        [SerializeField]
        private Vector3 maxRotation;
        [SerializeField] 
        private float rotateSpeed;

        [Header("Scale Boolean")] 
        private bool _isMovingUp;
        
        private void Awake()
        {
            _goingUpAnimationEffects.Add(new ScaleUpEffect(transform, maxScale, scaleSpeed));
            _goingUpAnimationEffects.Add(new RotateUpEffect(transform, maxRotation, rotateSpeed));
            _goingDownAnimationEffects.Add(new ScaleDownEffect(transform, minScale, scaleSpeed));
            _goingDownAnimationEffects.Add(new RotateDownEffect(transform, minRotation, rotateSpeed));
        }

        private void Update()
        {
            if(shapeSetup.GameManager.GameState != GameManager.State.Playing) return;
            switch (rb.velocity.y)
            {
                case > 0:
                    if(_isMovingUp) return;
                    StopAllCoroutines();
                    foreach (var effect in _goingUpAnimationEffects)
                    {
                        StartCoroutine(effect.Execute());
                    }
                    _isMovingUp = true;
                    break;
                case < 0:
                    if(!_isMovingUp) return;
                    StopAllCoroutines();
                    foreach (var effect in _goingDownAnimationEffects)
                    {
                        StartCoroutine(effect.Execute());
                    }
                    _isMovingUp = false;
                    break;
            }
        }
    }
}
