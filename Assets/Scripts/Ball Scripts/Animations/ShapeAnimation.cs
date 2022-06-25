using Manager;
using UnityEngine;

namespace Ball_Scripts.Animations
{
    public class ShapeAnimation : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private BallSetup ballSetup;

        private void FixedUpdate()
        {
            if(ballSetup.GameManager.GameState != GameManager.State.Playing) return;

            switch (rb.velocity.y)
            {
                case > 0:
                    anim.SetBool("isBallFalling", false);
                    break;
                case < 0:
                    anim.SetBool("isBallFalling", true);
                    break;
            }
        }
    }
}
