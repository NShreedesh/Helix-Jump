using Static;
using UnityEngine;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private int level;

        [SerializeField] 
        private int maxLevel;

        public void SaveLevel()
        {
            if (level >= maxLevel) return;
            
            IncrementLevel();
            PlayerPrefs.SetInt(SaveLoadTagManager.LevelNumberKey, level);
        }
        
        public int LoadLevel()
        {
            if (!PlayerPrefs.HasKey(SaveLoadTagManager.LevelNumberKey))
            {
                level = 1;
                return 1;
            }
            var levelNumber = PlayerPrefs.GetInt(SaveLoadTagManager.LevelNumberKey);
            if (levelNumber >= maxLevel)
            {
                level = maxLevel;
                return maxLevel;
            }

            level = levelNumber;
            return levelNumber;
        }

        private void IncrementLevel()
        {
            level++;
        }

        public void SetMaxLevel(int totalLevels)
        {
            maxLevel = totalLevels;
        }
    }
}
