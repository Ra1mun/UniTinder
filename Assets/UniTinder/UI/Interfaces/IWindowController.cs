using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWindowController
{ 
    Action GoToNextWindow { get; set; }
    Action GoToPreviousWindow { get; set; }

    void ShowWindow();
    void HideWindow();
}
