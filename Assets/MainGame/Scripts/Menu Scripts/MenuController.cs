using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class MenuController : MonoBehaviour {
    [SerializeField]
    private Slider brightnessSlider;
    [SerializeField]
    private Slider volumeSlider;

    // Multiplier for whole number volume and brightness settings to float value
    private static float floatSettingsMultiplier = .1f;
    // Multiplier for float number volume and brightness settings to whole number value
    private static float wholeNumberSettingsMultiplier = 10f;
    // Player pref settings
    private static float brightnessLevel = 0.0f;
    private static float volumeLevel = .5f;

    // Menu setup
    void Start()
    {
        // Defaults brightness to 0%
        brightnessLevel = PlayerPrefs.GetFloat(Utilities.BRIGHTNESSLEVELKEY, brightnessLevel);
        brightnessSlider.value = brightnessLevel * wholeNumberSettingsMultiplier;
        // Defaults volume to 50%
        volumeLevel = PlayerPrefs.GetFloat(Utilities.VOLUMELEVELKEY, volumeLevel);
        volumeSlider.value = volumeLevel * wholeNumberSettingsMultiplier;

        Time.timeScale = Utilities.TIMESCALEORIGINAL;
        Utilities.menuClosed = true;
    }

    // Exit/Quit the game
    public void Quit()
    {
        #if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
        #else
               Application.Quit();
        #endif
    }

    // Continues saved game / most recent player checkpoint
    public void ContinueSavedGame()
    {
        Utilities.sceneController.LoadGameScene(PlayerPrefs.GetString(Utilities.SAVEDGAMELEVELKEY, Utilities.currentScene));
    }

    // Activates submenu screen and hide main menu
    public void SubMenuOpen(GameObject subMenu)
    {
        subMenu.SetActive(true);
        Utilities.currentOpenMenu.SetActive(false);
        Utilities.currentOpenMenu = subMenu;
    }

    // Close submenu screen and show main menu
    public void SubMenuClose(GameObject subMenu)
    {
        subMenu.SetActive(false);
        Utilities.mainMenu.SetActive(true);
        Utilities.currentOpenMenu = Utilities.mainMenu;
    }

    // Change volume level
    public void SetVolume(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        AudioListener.volume = adjustedLevel;
        PlayerPrefs.SetFloat(Utilities.VOLUMELEVELKEY, adjustedLevel);
    }

    // Change brightness level
    public void SetBrightness(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        RenderSettings.ambientLight = new Color(adjustedLevel, adjustedLevel, adjustedLevel);
        PlayerPrefs.SetFloat(Utilities.BRIGHTNESSLEVELKEY, adjustedLevel);
    }
}
