using Manager;
using UnityEngine;

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

        private void Start()
        {
            gameManager.GameIdleAction += NoWinLoseUI;
            gameManager.GamePlayingAction += NoWinLoseUI;
            gameManager.GameLoseAction += LoseStateOn;
            gameManager.GameWinAction += WinStateOn;
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
        
        private void OnDisable()
        {
            gameManager.GameLoseAction -= LoseStateOn;
            gameManager.GameWinAction -= WinStateOn;
        }

    }
}
