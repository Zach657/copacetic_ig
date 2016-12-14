using UnityEngine;
using System.Collections;
/** 
 * Copyright (C) 2016 - Peter Wages
 **/
public class MinimapController : MonoBehaviour {

    // check for attempts to open / close minimap on m button press
    private void Update()
    {
        SceneController.ToggleMenuOnButtonPress(KeyCode.M, Utilities.minimap);
    }
}
