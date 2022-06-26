using UnityEngine;

namespace Player
{
    public class RotateHelix : MonoBehaviour
    {
        [Header("Other Components")] 
        [SerializeField]
        private InputController inputController;

        [Header("Helix Rotation Values")] 
        private float _speed;
        [SerializeField]
        private float mobileSensitivity;
        [SerializeField]
        private float pcSensitivity;

        [Header("Rotation Lerp Value")] 
        [SerializeField]
        [Range(0, 1)]
        private int lerpSpeed = 1;

        private void Start()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    _speed = mobileSensitivity;
                    break;
                default:
                    _speed = pcSensitivity;
                    break;
            }
        }

        private void Update()
        {
            if (!(inputController.MousePress > 0)) return;

            var xDelta = inputController.Delta.x;
            var eulerAngles = transform.eulerAngles;
                
            var smoothRotation = Vector3.Lerp(eulerAngles, new Vector3(0, xDelta * -_speed * Time.smoothDeltaTime, 0), lerpSpeed);
            eulerAngles += smoothRotation;
                
            transform.eulerAngles = eulerAngles;
        }
    }
}
