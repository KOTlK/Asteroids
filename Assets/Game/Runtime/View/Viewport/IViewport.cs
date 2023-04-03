using UnityEngine;

namespace Game.Runtime.View.Viewport
{
    public interface IViewport
    {
        Vector3 WorldToViewport(Vector3 worldPoint);
        Vector3 ViewportToWorld(Vector3 viewportPoint);
    }
}