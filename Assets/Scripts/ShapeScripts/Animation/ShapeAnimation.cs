using System;
using System.Collections.Generic;
using AnimationUtilities;
using Manager;
using UnityEngine;

namespace ShapeScripts.Animation
{
    public class ShapeAnimation : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private ShapeSetup shapeSetup;

        [Header("Animation Effect")] 
        public readonly List<IAnimationEffect> GoingUpAnimationEffects = new();
        public readonly List<IAnimationEffect> GoingDownAnimationEffects = new();

        [Header("Scale Boolean")] 
        private bool _isMovingUp;
        
        private void Update()
        {
            if(shapeSetup.GameManager.GameState != GameManager.State.Playing) return;
            switch (rb.velocity.y)
            {
                case > 0:
                    if(_isMovingUp) return;
                    StopAllCoroutines();
                    foreach (var effect in GoingUpAnimationEffects)
                    {
                        StartCoroutine(effect.Execute());
                    }
                    _isMovingUp = true;
                    break;
                case < 0:
                    if(!_isMovingUp) return;
                    StopAllCoroutines();
                    foreach (var effect in GoingDownAnimationEffects)
                    {
                        StartCoroutine(effect.Execute());
                    }
                    _isMovingUp = false;
                    break;
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
