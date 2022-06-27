using System;
using Static;
using UnityEngine;

namespace Player
{
    public class RotateHelix : MonoBehaviour
    {
        [Header("Other Components")] 
        [SerializeField]
        private InputController inputController;

        [Header("Actions")] 
        public Action OnSpeedChanged;

        [Header("Helix Rotation Values")]
        [SerializeField]
        private float initialSpeed;
        private float _speed;
        public float Speed 
        {
            get => _speed;
            private set
            {
                _speed = value;
                OnSpeedChanged?.Invoke();
            }
        }

        [Header("Rotation Lerp Value")] 
        [SerializeField]
        [Range(0, 1)]
        private int lerpSpeed = 1;

        private void Start()
        {
            Speed = LoadSensitivity();
        }

        private void Update()
        {
            if (!(inputController.MousePress > 0)) return;

            var xDelta = inputController.Delta.x;
            var eulerAngles = transform.eulerAngles;
                
            var smoothRotation = Vector3.Lerp(eulerAngles, new Vector3(0, xDelta * -Speed * Time.smoothDeltaTime, 0), lerpSpeed);
            eulerAngles += smoothRotation;
                
            transform.eulerAngles = eulerAngles;
        }

        public void ChangeSensitivity(float sensitivity)
        {
            Speed = sensitivity;
        }

        public void SaveSensitivity(float sensitivity)
        {
            PlayerPrefs.SetFloat(SaveLoadTagManager.RotateSpeedSetting, sensitivity);
        }
        
        private float LoadSensitivity()
        {
            return !PlayerPrefs.HasKey(SaveLoadTagManager.RotateSpeedSetting) 
                ? initialSpeed 
                : PlayerPrefs.GetFloat(SaveLoadTagManager.RotateSpeedSetting);
        }
    }
}
