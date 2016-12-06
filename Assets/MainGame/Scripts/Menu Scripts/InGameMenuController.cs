using UnityEngine;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class InGameMenuController : MenuController
{
    // Update is called once per frame
    private void Update () {
        // check for attempts to open / close in-game menu
        OpenCloseInGameMenuOnEscapeKeyDown();
    }

    // Opens the in game menu on escape key press
    private void OpenCloseInGameMenuOnEscapeKeyDown()
    {
        // On escape button press, pause or resume the game
        if (Input.GetKeyDown(KeyCode.Escape) && menuClosed && playerAlive)
        {   
            PauseGame(null);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !menuClosed && playerAlive)
        {
            ResumeGame();
        }
    }
}
