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

    // Level loading strings
    public const string LEVELONE = "environment-asylum";
    public const string LEVELTWO = "environment-outside";

    // Project-wide integer based booleans for use in Player Prefs ONLY
    public const int intTrue = 1;
    public const int intFalse = 0;

    // Scene information
    public static string currentScene;
    public Text notificationMessage;
    public GameObject notificationWindowSystem;

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

    // Fades the notification in and then fades it out
    IEnumerator DisplayNotificationForLimitedTime()
    {
        notificationWindowSystem.GetComponent<Image>().CrossFadeAlpha(visibleBackgroundAlpha, fadeDuration, false);
        notificationMessage.CrossFadeAlpha(visibleAlpha, fadeDuration, false);
        yield return new WaitForSeconds(10);
        notificationWindowSystem.GetComponent<Image>().CrossFadeAlpha(invisibleAlpha, fadeDuration, false);
        notificationMessage.CrossFadeAlpha(invisibleAlpha, fadeDuration, false);
    }

    // Plays audio clip at a location
    public void PlaySoundAtPoint(AudioClip audio, Vector3 location)
    {
        if (location == null)
        {
            location = GameObject.Find("PlayerCharacter").transform.position;
        }
        AudioSource.PlayClipAtPoint(audio, location);
    }
}
