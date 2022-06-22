using UnityEngine;

namespace Cylinder_Scripts
{
    public class RotateHelix : MonoBehaviour
    {
        [Header("Other Components")] 
        [SerializeField]
        private InputController inputController;

        [Header("Helix Rotation Values")] 
        [SerializeField]
        private float speed;

        private void Update()
        {
            if(inputController.MousePress <= 0) return;
            transform.Rotate(new Vector3(0, inputController.Delta.x * -speed * Time.deltaTime, 0));
        }
    }
}
