using Manager;
using UnityEngine;

namespace Cylinder_Scripts
{
    public class InputController : MonoBehaviour
    {
        [Header("Input")]
        private Input _input;
        
        [field:Header("Mouse Input Values")]
        public float MousePress { get; private set; }
        public Vector2 Delta { get; private set; }

        [Header("Managers")] 
        [SerializeField] 
        private GameManager gameManager;

        private void Awake()
        {
            _input = new Input();

        }

        private void Start()
        {
            gameManager.GameLoseAction += DisableInputController;
            gameManager.GameWinAction += DisableInputController;
            gameManager.GameIdleAction += DisableInputController;
            gameManager.GamePlayingAction += EnableInputController;
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
            
            gameManager.GameLoseAction -= DisableInputController;
            gameManager.GameWinAction -= DisableInputController;
            gameManager.GameIdleAction -= DisableInputController;
            gameManager.GamePlayingAction -= EnableInputController;
        }
        
        private void EnableInputController() => _input.Player.Enable();
        private void DisableInputController() => _input.Player.Disable();
    }
}
