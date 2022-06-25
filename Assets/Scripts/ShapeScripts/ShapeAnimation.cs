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
        private Animator anim;
        [SerializeField]
        private ShapeSetup shapeSetup;

        [Header("Parameters For Animation")] 
        private const string IsBallFalling = "isBallFalling";
        private readonly int _isBallFallingHash = Animator.StringToHash(IsBallFalling);

        private void FixedUpdate()
        {
            if(shapeSetup.GameManager.GameState != GameManager.State.Playing) return;

            switch (rb.velocity.y)
            {
                case > 0:
                    anim.SetBool(_isBallFallingHash, false);
                    break;
                case < 0:
                    anim.SetBool(_isBallFallingHash, true);
                    break;
            }
        }
    }
}
