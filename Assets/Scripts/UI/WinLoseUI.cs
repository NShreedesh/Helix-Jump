using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class WinLoseUI : MonoBehaviour
    {
        [Header("GameManager")]
        [SerializeField] 
        private GameManager gameManager;
        
        [Header("WinLose UI")]
        [SerializeField] 
        private RectTransform gameLoseUI;
        [SerializeField] 
        private RectTransform gameWinUI;

        [Header("WinLose Buttons")] 
        [SerializeField]
        private Button startAgainButton;
        [SerializeField]
        private Button nextLevelButton;

        private void Start()
        {
            gameManager.GameIdleAction += NoWinLoseUI;
            gameManager.GamePlayingAction += NoWinLoseUI;
            gameManager.GameLoseAction += LoseStateOn;
            gameManager.GameWinAction += WinStateOn;
            
            startAgainButton.onClick.AddListener(LoadLevel);
            nextLevelButton.onClick.AddListener(LoadLevel);
        }
        
        private void NoWinLoseUI()
        {
            gameLoseUI.gameObject.SetActive(false);
            gameWinUI.gameObject.SetActive(false);
        }
        
        private void LoseStateOn()
        {
            gameLoseUI.gameObject.SetActive(true);
        }

        private void WinStateOn()
        {
            gameWinUI.gameObject.SetActive(true);
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void OnDisable()
        {
            gameManager.GameLoseAction -= LoseStateOn;
            gameManager.GameWinAction -= WinStateOn;
            
            startAgainButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.RemoveAllListeners();
        }

    }
}
