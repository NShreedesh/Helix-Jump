using System.Collections.Generic;
using ShapeScripts;
using Camera;
using Data_Scripts;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generate
{
    public class GenerateGame : MonoBehaviour
    {
        [Header("Helix Info")]
        [SerializeField] 
        private LevelStorage[] levelStorage;
        [SerializeField] 
        private GameObject startHelix;
        [SerializeField] 
        private GameObject endHelix;
        [SerializeField] 
        private float cylinderSize = 4;
        private readonly List<GameObject> _spawnedHelixList = new();
        
        [Header("Shape Info")]
        [SerializeField] 
        private ShapeSetup[] shapeSetups;
        
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
        [SerializeField]
        private LevelManager levelManager;

        private void Awake()
        {
            levelManager.SetMaxLevel(levelStorage.Length);
        }

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
            _spawnedHelixList.Add(spawnedStartHelix);
            var level = levelManager.LoadLevel();
            
            foreach (var helix in levelStorage[level - 1].levelData)
            {
                var spawnedHelix = Instantiate(helix.helixToSpawn, thisTransform.position, Quaternion.identity, thisTransform);
                spawnedHelix.transform.localEulerAngles = spawnedHelix.transform.rotation.eulerAngles + new Vector3(0, (int)helix.rotationY, 0);
                spawnedHelix.SetActive(false);
                _spawnedHelixList.Add(spawnedHelix);
            }
            
            var spawnedEndHelix = Instantiate(endHelix, thisTransform.position, Quaternion.identity, thisTransform);
            spawnedEndHelix.SetActive(false);
            _spawnedHelixList.Add(spawnedEndHelix);
        }

        private void LevelMaking()
        {
            var position = transform.position;
            for (var i = 0; i < _spawnedHelixList.Count; i++)
            {
                _spawnedHelixList[i].transform.position = new Vector3(position.x, position.y - (cylinderSize * i), position.z);
                _spawnedHelixList[i].SetActive(true);
            }
        }

        private void SpawnBall()
        {
            var randomShape = Random.Range(0, shapeSetups.Length);
            var ball = Instantiate(shapeSetups[randomShape], _spawnedHelixList[0].transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            
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
