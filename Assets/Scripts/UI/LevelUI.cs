using Manager;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelUI : MonoBehaviour
    {
        [Header("Text Components")] 
        [SerializeField]
        private TMP_Text levelText;

        [Header("Manager")]
        [SerializeField] 
        private LevelManager levelManager;

        private void Start()
        {
            levelManager.OnLevelChange += ChangeLevelText;
        }

        private void ChangeLevelText(int levelNumber)
        {
            levelText.text = levelNumber.ToString();
        }

        private void OnDisable()
        {
            levelManager.OnLevelChange -= ChangeLevelText;
        }
    }
}
