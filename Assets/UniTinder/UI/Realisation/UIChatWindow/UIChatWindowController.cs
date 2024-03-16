using System;
using UnityEngine;
using Zenject;
using Zenject.ReflectionBaking.Mono.Cecil;

namespace UniTinder.UI.Realisation
{
    public class UIChatWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly IInstantiator instantiator;
        private readonly UIChatWindow _uiChatWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIChatWindowController(UIService.UIService uiService,
            IInstantiator instantiator)
        {
            _uiService = uiService;
            this.instantiator = instantiator;
            _uiChatWindow = _uiService.Get<UIChatWindow>();
        }

        public void ShowWindow()
        {
            _uiChatWindow.GoToPreviousWindowEvent += GoToPrevious;
            _uiChatWindow.SendMessageEvent += SendMessage;
            
            _uiService.Show<UIChatWindow>();
        }

        public void RecieveMessage(string text)
        {

        }

        private void SendMessage(string text)
        {
            if (text == null)
            {
                Debug.LogError("Message is null");
                
                return;
            }

            var view = instantiator
                .InstantiatePrefabResourceForComponent<MessageView>("UIElements");
            view.SetText(text);

            _uiChatWindow.AddMessage(view);
        }

        private void GoToPrevious()
        {
            GoToPreviousWindow?.Invoke();
            
            HideWindow();
        }
        
        public void HideWindow()
        {
            _uiChatWindow.GoToPreviousWindowEvent -= GoToPrevious;
            _uiChatWindow.SendMessageEvent -= SendMessage;
            
            _uiService.Hide<UIChatWindow>();
        }
    }
}