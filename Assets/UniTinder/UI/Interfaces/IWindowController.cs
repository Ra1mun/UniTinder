using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWindowController
{ 
    Action GoToNext { get; set; }
    Action GoToPrevious { get; set; }

    void ShowWindow();
    void HideWindow();
}
