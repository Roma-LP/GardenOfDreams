using UnityEngine;

namespace _GardenOfDreams.Scripts.UI.HP
{
    public class WorldToUIFollower
    {
        private Transform _pivot;
        private Camera _camera;
        private RectTransform _rectTransform;

        public WorldToUIFollower(RectTransform rectTransformHealthBar, Transform pivot)
        {
            _pivot = pivot;
            _camera = Camera.main;
            _rectTransform = rectTransformHealthBar;
        }

        public void LateUpdateUIFollower()
        {
            if (_pivot == null) return;

            Vector3 screenPos = _camera.WorldToScreenPoint(_pivot.position);
            _rectTransform.position = screenPos;
        }
    }
}