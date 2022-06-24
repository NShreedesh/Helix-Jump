using System.Collections.Generic;
using Ball_Scripts;
using Camera;
using Data_Scripts;
using Manager;
using UnityEngine;

namespace Generate
{
    public class GenerateGame : MonoBehaviour
    {
        [Header("Helix Info")]
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
        
        [Header("Ball Info")]
        [SerializeField] 
        private BallSetup ballSetup;
        
        [Header("Camera Info")]
        [SerializeField] 
        private CameraController cameraController;

        [Header("Manager Info")]
        [SerializeField] 
        private AudioManager audioManager;
        [SerializeField] 
        private ScoreManager scoreManager;
        [SerializeField] 
        private GameColorManager gameColorManager;
        [SerializeField]
        private GameManager gameManager;

        private void Start()
        {
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
            var ball = Instantiate(ballSetup, spawnedHelixList[0].transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            
            var ballJumpTransform = ball.transform;
            var ballPosition = ballJumpTransform.position;
            ballPosition.z = -2f;
            
            ballJumpTransform.position = ballPosition;
            
            ball.SetAudioManager(audioManager);
            ball.SetScoreManager(scoreManager);
            ball.SetSplashColor(gameColorManager.SplashColor);
            ball.SetGameManager(gameManager);
            cameraController.AssignTarget(ballJumpTransform);
            
            gameManager.ChangeGameState(GameManager.State.Playing);
        }
    }
}
