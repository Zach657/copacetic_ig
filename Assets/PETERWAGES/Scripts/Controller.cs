using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class Controller : MonoBehaviour {
    // Player pref keys
    const string KEYBASE = "CopaceticGamesCo-Noises-Game-";
    public const string gameInProgressKey = KEYBASE + "GameInProgressKey";
    public const string brightnessLevelKey = KEYBASE + "BrightnessLevelKey";
    public const string volumeLevelKey = KEYBASE + "VolumeLevelKey";
    public const string savedGameSceneKey = KEYBASE + "SavedGameSceneKey";

    // Project-wide integer based booleans for use in Player Prefs ONLY
    public const int intTrue = 1;
    public const int intFalse = 0;

    // Scene information
    public static string currentScene;

    [HideInInspector]
    public static bool playerAlive = true;

    // Loads a specific scene
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Saves the current game and allows it to be loadable when continue button in main menu is pressed
    public void SaveGame()
    {
        PlayerPrefs.SetInt(gameInProgressKey, intTrue);
        PlayerPrefs.SetString(savedGameSceneKey, currentScene);
    }
}
