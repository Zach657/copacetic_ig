using UnityEngine;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
[RequireComponent(typeof(MinimapController))]
[RequireComponent(typeof(NumpadEntryController))]
[RequireComponent(typeof(CompassContoller))]
public class InGameMenuController : MenuController
{
    // Update is called once per frame
    private void Update () {
        // check for attempts to open / close in-game menu on escape press
        SceneController.ToggleMenuOnButtonPress(KeyCode.Escape, null);
    }
}
