using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
    public Text notificationMessage;
    public GameObject notificationWindow;

    [HideInInspector]
    public static bool playerAlive = true;

    // CrossFadeAlpha refuses to work if Alpha is set to 0, so setting to 1
    private float invisibleAlpha = 1f;
    private float visibleAlpha = 255f;
    private float visibleBackgroundAlpha = 150f;
    private float fadeDuration = 1.25f;

    // Loads a specific scene
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Saves the current game and allows it to be loadable when continue button in main menu is pressed
    public void SaveGame()
    {
        PlayerPrefs.SetInt(gameInProgressKey, intTrue);
        PlayerPrefs.SetString(savedGameSceneKey, currentScene);
    }

    // Displays a notification at top of screen
    public void DisplayNotification(string message)
    {
        notificationMessage.text = message;
        StartCoroutine(DisplayNotificationForLimitedTime());
    }

    // Fadesthe notification in and then fades it out
    IEnumerator DisplayNotificationForLimitedTime()
    {
        notificationWindow.GetComponent<Image>().CrossFadeAlpha(visibleBackgroundAlpha, fadeDuration, false);
        notificationMessage.CrossFadeAlpha(visibleAlpha, fadeDuration, false);
        yield return new WaitForSeconds(5);
        notificationWindow.GetComponent<Image>().CrossFadeAlpha(invisibleAlpha, fadeDuration, false);
        notificationMessage.CrossFadeAlpha(invisibleAlpha, fadeDuration, false);
    }

    // Plays audio clip at a location
    public void PlaySoundAtPoint(AudioClip audio, Vector3 location)
    {
        AudioSource.PlayClipAtPoint(audio, location);
    }
}
