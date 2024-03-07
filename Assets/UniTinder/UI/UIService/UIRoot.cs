using UnityEngine;

namespace UniTinder.UI.UIService
{
    public class UIRoot : MonoBehaviour
    {
        public Canvas Canvas => _canvas;
        public RectTransform Container => _container;
        public RectTransform PoolContainer => _poolContainer;
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _container;
        [SerializeField] private RectTransform _poolContainer;
    }
}