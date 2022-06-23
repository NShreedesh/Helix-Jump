using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor.Helix_Scripts
{
    public class EditorHelixGenerate : MonoBehaviour
    {
        [SerializeField] 
        private GameObject whichHelix;

        [SerializeField] 
        private GameObject[] helixes;

        public void GenerateAndRotate()
        {
            if (transform.childCount > 0)
            {
                ClearAllHelixes();
            }
            
            for (var i = 0; i < 12; i++)
            {
               var prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(whichHelix);
               var helix = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(Object));
               var prefabInstance = PrefabUtility.InstantiatePrefab(helix, this.transform) as GameObject;
               
               if (prefabInstance == null) continue;
               prefabInstance.transform.position = new Vector3(0, 0, 0);
               prefabInstance.transform.rotation = Quaternion.Euler(-90, 0, i * 30);
            }
        }
        
        public void GenerateRandomHelix()
        {
            if (transform.childCount > 0)
            {
                ClearAllHelixes();
            }
            
            for (var i = 0; i < 12; i++)
            {
                var randomHelix = Random.Range(0, helixes.Length);
                var prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(helixes[randomHelix]);
                var helix = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(Object));
                var prefabInstance = PrefabUtility.InstantiatePrefab(helix, this.transform) as GameObject;
                
                if (prefabInstance == null) continue;
                prefabInstance.transform.position = new Vector3(0, 0, 0);
                prefabInstance.transform.rotation = Quaternion.Euler(-90, 0, i * 30);
            }
        }
        
        public void ClearAllHelixes()
        {
            var tempList = transform.Cast<Transform>().ToList();
            foreach (var child in tempList)
            { 
                DestroyImmediate(child.gameObject);
            }
        }
    }
}
