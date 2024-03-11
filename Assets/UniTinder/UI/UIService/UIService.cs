using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UniTinder.UI.UIService
{
    public class UIService
    {
        private readonly IInstantiator _instantiator;
        private readonly UIRoot _uiRoot;
    
        private readonly Dictionary<Type, UIWindow> _viewStorage = new Dictionary<Type, UIWindow>();
        private readonly Dictionary<Type, GameObject> _instViews = new Dictionary<Type, GameObject>();

        public UIService(
            IInstantiator instantiator,
            UIRoot uiRoot)
        {
            _instantiator = instantiator;
            _uiRoot = uiRoot;
        }

        public void LoadWindows()
        {
            var windows = Resources.LoadAll("UIWindows", typeof(UIWindow));
            
            for (int i = 0; i < windows.Length; i++)
            {
                _viewStorage.Add(windows[i].GetType(), (UIWindow)windows[i]);
            }
        }

        public void InitWindows()
        {
            foreach (var uiWindow in _viewStorage)
            {
                Init(uiWindow.Key, _uiRoot.PoolContainer);
            }
        }
    
        public void Show<T>() where T : UIWindow
        {
            var type = typeof(T);
            if (_instViews.ContainsKey(type))
            {
                var view = _instViews[type];

                view.transform.SetParent(_uiRoot.Container, false);
                
                view.transform.localScale = Vector3.one;
                view.transform.localRotation = Quaternion.identity;
                view.transform.localPosition = Vector3.zero;

                var component = view.GetComponent<T>();
                
                var rect = component.transform as RectTransform;
                if (rect != null)
                {
                    rect.offsetMin = Vector2.zero;
                    rect.offsetMax = Vector2.zero;
                }
                
                component.Show();
            }
        }
        
        public void Hide<T>(Action onEnd = null) where T : UIWindow
        {
            var type = typeof(T);
            if (_instViews.ContainsKey(type))
            {
                var view = _instViews[type];
                var viewComponent = view.GetComponent<T>();

                view.transform.SetParent(_uiRoot.PoolContainer);
                onEnd?.Invoke();

                viewComponent.Hide();
            }
        }

        private void Init(Type type, Transform parent = null)
        {
            if (_viewStorage.ContainsKey(type) && !_instViews.ContainsKey(type))
            {
                GameObject view;

                if (parent == null)
                {
                    view = _instantiator.InstantiatePrefab(_viewStorage[type]);
                }
                else
                {
                    view = _instantiator.InstantiatePrefab(_viewStorage[type], parent);
                }
            
                _instViews.Add(type, view);
            }
        }
        
        public T Get<T>() where T : UIWindow
        {
            var type = typeof(T);
            if (_instViews.ContainsKey(type))
            {
                var view = _instViews[type];
                var viewComponent = view.GetComponent<T>();

                return viewComponent;
            }

            return null;
        }
    }
}
