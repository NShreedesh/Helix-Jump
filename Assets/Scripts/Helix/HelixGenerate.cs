using System.Collections.Generic;
using Audio;
using Ball;
using Camera;
using Data_Scripts;
using UnityEngine;

namespace Helix
{
    public class HelixGenerate : MonoBehaviour
    {
        [SerializeField] 
        private LevelStorage levelStorage;
        [SerializeField] 
        private GameObject startHelix;
        [SerializeField] 
        private GameObject endHelix;
        [SerializeField] 
        private float cylinderSize = 4;
        [SerializeField] 
        private List<GameObject> spawnedHelixList;
        [SerializeField] 
        private BallJump ball;
        [SerializeField] 
        private CameraController cameraController;

        [SerializeField] 
        private AudioManager audioManager;

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

        private void Start()
        {
            SetColors();
            Generate();
            LevelMaking();
            SpawnBall();
        }

        private void Generate()
        {
            var thisTransform = transform;
            
            var spawnedStartHelix = Instantiate(startHelix, thisTransform.position, Quaternion.identity, thisTransform);
            spawnedStartHelix.SetActive(false);
            spawnedHelixList.Add(spawnedStartHelix);
            
            foreach (var helix in levelStorage.levelData)
            {
                var spawnedHelix = Instantiate(helix.helixToSpawn, thisTransform.position, Quaternion.identity, thisTransform);
                spawnedHelix.transform.localEulerAngles = spawnedHelix.transform.rotation.eulerAngles + new Vector3(0, (int)helix.rotationY, 0);
                spawnedHelix.SetActive(false);
                spawnedHelixList.Add(spawnedHelix);
            }
            
            var spawnedEndHelix = Instantiate(endHelix, thisTransform.position, Quaternion.identity, thisTransform);
            spawnedEndHelix.SetActive(false);
            spawnedHelixList.Add(spawnedEndHelix);
        }

        private void LevelMaking()
        {
            var position = transform.position;
            for (var i = 0; i < spawnedHelixList.Count; i++)
            {
                spawnedHelixList[i].transform.position = new Vector3(position.x, position.y - (cylinderSize * i), position.z);
                spawnedHelixList[i].SetActive(true);
            }
        }

        private void SpawnBall()
        {
            var ballJump = Instantiate(ball, spawnedHelixList[0].transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            
            var ballJumpTransform = ballJump.transform;
            var ballPosition = ballJumpTransform.position;
            ballPosition.z = -2.7f;
            
            ballJumpTransform.position = ballPosition;
            ballJump.audioManager = audioManager;
            
            cameraController.AssignTarget(ballJumpTransform);
        }

        private void SetColors()
        {
            var randomColorArray = Random.Range(0, colorStorage.colorData.Length);
            ballMaterial.color = colorStorage.colorData[randomColorArray].ballColor;
            nonKillHelixMaterial.color = colorStorage.colorData[randomColorArray].nonKillHelixColor;
            killHelixMaterial.color = colorStorage.colorData[randomColorArray].killHelixColor;
            levelCompleteHelixMaterial.color = colorStorage.colorData[randomColorArray].levelCompleteHelixColor;
        }
    }
}
