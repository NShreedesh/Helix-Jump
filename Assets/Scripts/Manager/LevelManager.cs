using Static;
using UnityEngine;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private int level;
        public int Level
        {
            get => level;
            set => level = value;
        }

        private void Start()
        {
            level = LoadLevel();
        }

        public void SaveLevel()
        {
            PlayerPrefs.SetInt(SaveLoadTagManager.LevelNumberKey, level);
        }
        
        private int LoadLevel()
        {
            if (!PlayerPrefs.HasKey(SaveLoadTagManager.LevelNumberKey)) return 1;

            var levelNumber = PlayerPrefs.GetInt(SaveLoadTagManager.LevelNumberKey);
            return levelNumber;
        }
    }
}
