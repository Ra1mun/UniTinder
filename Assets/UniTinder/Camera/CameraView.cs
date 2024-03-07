using UniTinder.SceneObjectStorage.Scripts;
using UnityEngine;

namespace UniTinder.Camera
{
    public class CameraView : SceneObject
    {
        public UnityEngine.Camera CameraDisplay => _cameraDisplay;
        
        [SerializeField] private UnityEngine.Camera _cameraDisplay;
    }
}