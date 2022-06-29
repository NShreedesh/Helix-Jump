using Manager;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] 
        private RotateHelix rotateHelix;
        [SerializeField] 
        private GameManager gameManager;
        
        [Header("Settings UI")] 
        [SerializeField]
        private Slider slider;

        [Header("Open/Close Menu")] 
        [SerializeField]
        private GameObject menu;
        [SerializeField]
        private Button openMenuButton;
        [SerializeField]
        private Button closeMenuButton;

        [Header("Store State")]
        public GameManager.State previousState;
        
        private void Awake()
        {
            menu.SetActive(false);
        }

        private void Start()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            openMenuButton.onClick.AddListener(OpenMenuButtonClicked);
            closeMenuButton.onClick.AddListener(CloseMenuButtonClicked);
            
            RotateHelix.OnSpeedChanged += OnSpeedValueChanged;
        }

        private void OnSliderValueChanged(float value)
        {
            rotateHelix.ChangeSensitivity(value * 100);
        }

        private void OnSpeedValueChanged()
        {
            slider.value = rotateHelix.Speed / 100;
        }

        private void OpenMenuButtonClicked()
        {
            previousState = gameManager.GameState;
            gameManager.ChangeGameState(GameManager.State.Idle);
            Time.timeScale = 0;
            menu.SetActive(true);
            openMenuButton.gameObject.SetActive(false);
        }

        private void CloseMenuButtonClicked()
        {
            gameManager.ChangeGameState(previousState);
            rotateHelix.SaveSensitivity(slider.value * 100);
            Time.timeScale = 1;
            menu.SetActive(false);
            openMenuButton.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            slider.onValueChanged.RemoveAllListeners();
            openMenuButton.onClick.RemoveAllListeners();
            closeMenuButton.onClick.RemoveAllListeners();
            
            RotateHelix.OnSpeedChanged -= OnSpeedValueChanged;
        }
    }
}
