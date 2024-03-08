using System.Collections;
using System.Collections.Generic;
using UniTinder.UI.Realisation.UIStartWindow;
using UnityEngine;

public class UIWindowsController
{
    private readonly LinkedList<IWindowController> _windowControllers = new LinkedList<IWindowController>();

    public UIWindowsController(
        UIStartWindowController uiStartWindowController)
    {
        SetupWindow(uiStartWindowController);
    }

    public void ShowFirstWindow()
    {
        _windowControllers.First.Value.ShowWindow();
    }

    private void SetupWindow(IWindowController windowController)
    {
        if (_windowControllers.Last != null)
        {
            _windowControllers.Last.Value.GoToNext += windowController.ShowWindow;
            
            windowController.GoToPrevious += _windowControllers.Last.Value.ShowWindow;
        }

        _windowControllers.AddLast(windowController);
    }
}
