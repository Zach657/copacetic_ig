using UnityEngine;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class InGameMenuController : MenuController
{
    // Boolean for checking if in-game menu is closed
    private bool menuClosed = true;

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
            menuClosed = false;
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !menuClosed && playerAlive)
        {
            menuClosed = true;
            ResumeGame();
        }
    }
}
