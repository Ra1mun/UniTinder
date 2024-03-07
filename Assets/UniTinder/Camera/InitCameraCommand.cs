using System;
using UniTinder.Bootstrap.Interfaces;
using UniTinder.SceneObjectStorage;
using UniTinder.UI;
using UniTinder.UI.UIService;

namespace UniTinder.Camera
{
    public class InitCameraCommand : ICommand
    {
        private readonly SceneObjectStorage.Scripts.SceneObjectStorage _sceneObjectStorage;
        private readonly UIRoot _uiRoot;
        public Action Done { get; set; }

        public InitCameraCommand(
            SceneObjectStorage.Scripts.SceneObjectStorage sceneObjectStorage,
            UIRoot uiRoot)
        {
            _sceneObjectStorage = sceneObjectStorage;
            _uiRoot = uiRoot;
        }
        
        public void Execute()
        {
            var cameraView = _sceneObjectStorage.CreateAndAdd<CameraView>("CameraView");

            _uiRoot.Canvas.worldCamera = cameraView.CameraDisplay;
            
            Done?.Invoke();
        }
    }
}