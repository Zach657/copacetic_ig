using UnityEngine;
using System.Collections;

public class MinimapController : MenuController {

    // Update is called once per frame
    private void Update()
    {
        // check for attempts to open / close in-game menu
        OpenCloseMinimapOnMKeyDown();
    }

    // Opens the in game menu on escape key press
    private void OpenCloseMinimapOnMKeyDown()
    {
        // On escape button press, pause or resume the game
        if (Input.GetKeyDown(KeyCode.M) && menuClosed && playerAlive)
        {
            PauseGame(null);
        }
        else if (Input.GetKeyDown(KeyCode.M) && !menuClosed && playerAlive)
        {
            ResumeGame();
        }
    }
}
