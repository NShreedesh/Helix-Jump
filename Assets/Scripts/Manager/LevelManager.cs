using System;
using Static;
using UI;
using UnityEngine;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private int level;

        [SerializeField] 
        private int maxLevel;

        public Action<int> OnLevelChange;

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
                LevelChanged(level);
                return 1;
            }
            var levelNumber = PlayerPrefs.GetInt(SaveLoadTagManager.LevelNumberKey);
            if (levelNumber >= maxLevel)
            {
                level = maxLevel;
                LevelChanged(level);
                return maxLevel;
            }

            level = levelNumber;
            LevelChanged(level);
            return levelNumber;
        }

        private void IncrementLevel()
        {
            level++;
        }

        private void LevelChanged(int levelNumber)
        {
            OnLevelChange?.Invoke(levelNumber);
        }

        public void SetMaxLevel(int totalLevels)
        {
            maxLevel = totalLevels;
        }
    }
}
