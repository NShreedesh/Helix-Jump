using System.Collections.Generic;
using Ball;
using Camera;
using UnityEngine;

namespace Helix
{
    public class HelixGenerate : MonoBehaviour
    {
        [SerializeField] 
        private LevelStorage levelStorage;
        [SerializeField] 
        private float cylinderSize = 4;
        [SerializeField] 
        private List<GameObject> spawnedHelixList;
        [SerializeField] 
        private BallJump ball;
        [SerializeField] 
        private CameraController cameraController;

        private void Start()
        {
            Generate();
            LevelMaking();
            SpawnBall();
        }

        private void Generate()
        {
            var thisTransform = transform;

            foreach (var helix in levelStorage.levelData)
            {
                var spawnedHelix = Instantiate(helix.helixToSpawn, thisTransform.position, Quaternion.identity, thisTransform);
                spawnedHelix.transform.localEulerAngles = spawnedHelix.transform.rotation.eulerAngles + new Vector3(0, helix.rotationY, 0);
                spawnedHelix.SetActive(false);
                spawnedHelixList.Add(spawnedHelix);
            }
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
            ballPosition.z = 0.5f;
            
            ballJumpTransform.position = ballPosition;
            
            cameraController.AssignTarget(ballJumpTransform);
        }
    }
}
