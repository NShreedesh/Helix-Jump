using UnityEngine;

namespace UI
{
    public class SafeUIArea : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;

        private Rect _safeArea;
        private Vector2 _minAnchor;
        private Vector2 _maxAnchor;
        
        private void Awake()
        {
            _safeArea = Screen.safeArea;
            _minAnchor = _safeArea.position;
            _maxAnchor = _minAnchor + _safeArea.size;

            _minAnchor.x /= Screen.width;
            _minAnchor.y /= Screen.height;
            _maxAnchor.x /= Screen.width;
            _maxAnchor.y /= Screen.height;

            rectTransform.anchorMin = _minAnchor;
            rectTransform.anchorMax = _maxAnchor;
        }
    }
}
