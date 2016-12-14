using UnityEngine;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
[RequireComponent(typeof(MinimapController))]
[RequireComponent(typeof(NumpadEntryController))]
[RequireComponent(typeof(CompassContoller))]
public class InGameMenuController : MenuController
{
    // check for attempts to open / close in-game menu on escape press
    private void Update () {
        SceneController.ToggleMenuOnButtonPress(KeyCode.Escape, null);
    }
}
