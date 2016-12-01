using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class MainMenuController : MenuController
{
    [SerializeField]
    private GameObject newGameButton;
    [SerializeField]
    private GameObject continueGameButton;
    private Vector3 centerNewGameNoContinue = new Vector3(0f, 50f, 0f);

    // Specific setups for main menu only
    // Hides/destroys continue button if no saved game is found
    private void Start()
    {
        // 1 is true, 0 is false, since PlayerPrefs does not support booleans natively
        if (PlayerPrefs.GetInt(gameInProgressKey, intFalse) == intFalse)
        {
            newGameButton.GetComponent<RectTransform>().anchoredPosition3D = centerNewGameNoContinue;
            Destroy(continueGameButton);
        }
    }
}