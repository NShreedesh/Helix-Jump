using Data_Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Color Change")] 
        [SerializeField]
        private ColorStorage colorStorage;
        [SerializeField] 
        private Material ballMaterial;
        [SerializeField]
        private Material nonKillHelixMaterial;
        [SerializeField] 
        private Material killHelixMaterial;
        [SerializeField] 
        private Material levelCompleteHelixMaterial;
        
        public Color32 SplashColor { get; private set; }

        private void Start()
        {
            SetColors();
        }
        
        private void SetColors()
        {
            var randomColorArray = Random.Range(0, colorStorage.colorData.Length);
            ballMaterial.color = colorStorage.colorData[randomColorArray].ballColor;
            nonKillHelixMaterial.color = colorStorage.colorData[randomColorArray].nonKillHelixColor;
            killHelixMaterial.color = colorStorage.colorData[randomColorArray].killHelixColor;
            levelCompleteHelixMaterial.color = colorStorage.colorData[randomColorArray].levelCompleteHelixColor;

            SplashColor = ballMaterial.color;
        }
    }
}
