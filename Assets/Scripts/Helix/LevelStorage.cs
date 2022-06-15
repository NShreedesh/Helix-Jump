using System;
using UnityEngine;

namespace Helix
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
        public float rotationY;
    }
}
