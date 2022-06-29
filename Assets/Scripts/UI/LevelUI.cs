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

        private void Start()
        {
            LevelManager.OnLevelChange += ChangeLevelText;
        }

        private void ChangeLevelText(int levelNumber)
        {
            levelText.text = levelNumber.ToString();
        }

        private void OnDisable()
        {
            LevelManager.OnLevelChange -= ChangeLevelText;
        }
    }
}
