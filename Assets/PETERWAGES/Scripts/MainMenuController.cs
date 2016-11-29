using UnityEngine;
using System.Collections;

public class MainMenuController : MenuController
{
    [SerializeField]
    private GameObject newGameButton;
    [SerializeField]
    private GameObject continueGameButton;

    private Vector3 centerNewGameNoContinue = new Vector3(0f, 50f, 0f);
    private bool gameInProgress = false;

    // Specific setups for main menu only
    void Start()
    {
        // 1 is true, 0 is false, since PlayerPrefs does not support booleans natively
        gameInProgress = (PlayerPrefs.GetInt(gameInProgressKey, 0) == 1);
        if (!gameInProgress)
        {
            newGameButton.GetComponent<RectTransform>().anchoredPosition3D = centerNewGameNoContinue;
            Destroy(continueGameButton);
        }
    }
}