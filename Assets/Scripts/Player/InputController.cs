using Manager;
using UnityEngine;

namespace Player
{
    public class InputController : MonoBehaviour
    {
        [Header("Input")]
        private Input _input;
        
        [field:Header("Mouse Input Values")]
        public float MousePress { get; private set; }
        public Vector2 Delta { get; private set; }

        private void Awake()
        {
            _input = new Input();
        }

        private void Start()
        {
            GameManager.GameLoseAction += DisableInputController;
            GameManager.GameWinAction += DisableInputController;
            GameManager.GameIdleAction += DisableInputController;
            GameManager.GamePlayingAction += EnableInputController;
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
            
            GameManager.GameLoseAction -= DisableInputController;
            GameManager.GameWinAction -= DisableInputController;
            GameManager.GameIdleAction -= DisableInputController;
            GameManager.GamePlayingAction -= EnableInputController;
        }
        
        private void EnableInputController() => _input.Player.Enable();
        
        private void DisableInputController() => _input.Player.Disable();
    }
}
