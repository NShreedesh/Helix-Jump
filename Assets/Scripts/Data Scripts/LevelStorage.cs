using System;
using UnityEngine;

namespace Data_Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Levels", order = 1)]
    public class LevelStorage : ScriptableObject
    {
        public LevelData[] levelData;
    }
    
    [Serializable]
    public class LevelData
    {
        public GameObject helixToSpawn;
        public RotationY rotationY = RotationY.Zero;

        public enum RotationY
        {
            Zero = 0,
            Thirty = 30,
            Sixty = 60,
            Ninety = 90,
            OneTwenty = 120,
            OneFifty = 150,
            OneEighty = 180,
            TwoTen = 210,
            TwoForty = 240,
            TwoSeventy = 270,
            ThreeHundred = 300,
            ThreeThirty = 330
        }
    }
}
