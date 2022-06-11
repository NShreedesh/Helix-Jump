using UnityEngine;

namespace Player
{
    public class InputController : MonoBehaviour
    {
        private Input _input;
        
        [field:Header("Mouse Input Values")]
        public float MousePress { get; private set; }
        public Vector2 Delta { get; private set; }

        private void Awake()
        {
            _input = new Input();
        }

        private void OnEnable()
        {
            _input.Player.MousePress.started += ctx => MousePress = ctx.ReadValue<float>();
            _input.Player.MousePress.canceled += ctx => MousePress = ctx.ReadValue<float>();
            
            _input.Player.MouseDelta.performed += ctx => Delta = ctx.ReadValue<Vector2>();
            _input.Player.MouseDelta.canceled += ctx => Delta = ctx.ReadValue<Vector2>();
            
            _input.Player.Enable();
        }

        private void OnDisable()
        {
            _input.Player.Disable();
        }
    }
}
