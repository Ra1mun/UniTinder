using UniTinder.SceneObjectStorage.Scripts;
using UnityEngine;

namespace UniTinder.Camera
{
    public class CameraView : SceneObject
    {
        public UnityEngine.Camera CameraDisplay => cameraDisplay;
        
        [SerializeField] private UnityEngine.Camera cameraDisplay;
    }
}