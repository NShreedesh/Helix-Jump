using System;
using Unity.Mathematics;
using UnityEngine;

namespace Helix
{
    public class HelixGenerate : MonoBehaviour
    {
        [Header("Helix Info")] 
        [SerializeField]
        private GameObject helix;
        [SerializeField] 
        private Transform helixSpawnParent;

        private void Start()
        {
            SpawnHelix();
        }

        private void SpawnHelix()
        {
            Instantiate(helix, new Vector3(0, 38, 0), quaternion.identity, helixSpawnParent);
        }
    }
}
