using System;
using UnityEngine;

namespace Data_Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Colors", order = 1)]
    public class ColorStorage : ScriptableObject
    {
        public ColorData colorData;
    }

    [Serializable]
    public class ColorData
    {
        [Header("Helix Colors")]
        public Color nonKillHelixColor;
        public Color killHelixColor;
        public Color levelCompleteHelixColor;

        [Header("Ball Color")] 
        public Color ballColor;
    }
}
