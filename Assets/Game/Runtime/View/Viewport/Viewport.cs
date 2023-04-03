using UnityEngine;

namespace Game.Runtime.View.Viewport
{
    public class Viewport : MonoBehaviour, IViewport
    {
        [SerializeField] private Camera _camera;
        
        public Vector3 WorldToViewport(Vector3 worldPoint)
        {
            return _camera.WorldToViewportPoint(worldPoint);
        }

        public Vector3 ViewportToWorld(Vector3 viewportPoint)
        {
            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}