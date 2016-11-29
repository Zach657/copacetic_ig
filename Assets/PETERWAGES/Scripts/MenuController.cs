using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MenuController : MonoBehaviour {
    public GameObject mainMenu;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider brightnessSlider;

    // Multiplier for whole number volume and brightness settings to float value
    private float floatSettingsMultiplier = .1f;
    // Multiplier for float number volume and brightness settings to whole number value
    private float wholeNumberSettingsMultiplier = 10f;
    
    // Boolean for checking if in-game menu is closed
    public bool menuClosed = true;

    // Currrent open menu
    public GameObject currentOpenMenu;

    public Camera mainCamera;
    public BlurOptimized blurEffectMainCamera;


    // Player pref keys
    private string keyBase = "CopaceticGamesCo-Noises-Game-";
    public string gameInProgressKey;
    public string brightnessLevelKey;
    public string volumeLevelKey;

    // Player pref settings
    private float brightnessLevel = 0.0f;
    private float volumeLevel = .5f;

    // Menu shared setup
    void Start()
    {
        gameInProgressKey = keyBase + "GameInProgressKey";
        brightnessLevelKey = keyBase + "BrightnessLevelKey";
        volumeLevelKey = keyBase + "VolumeLevelKey";

        // Defaults brightness to 0%
        brightnessLevel = PlayerPrefs.GetFloat(brightnessLevelKey, brightnessLevel);
        brightnessSlider.value = brightnessLevel * wholeNumberSettingsMultiplier;
        // Defaults volume to 50%
        volumeLevel = PlayerPrefs.GetFloat(volumeLevelKey, volumeLevel);
        volumeSlider.value = volumeLevel * wholeNumberSettingsMultiplier;
    }

    // Exit/Quit the game - Does not work in Unity Editor, works in builds only
    public void Quit()
    {
        Application.Quit();
    }

    // Loads a specific scene
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Continues saved game
    public void ContinueSavedGame()
    {
        // High target goal, may not be implemented
    }

    // Activates submenu screen and hide main menu
    public void SubMenuOpen(GameObject subMenu)
    {
        subMenu.SetActive(true);
        mainMenu.SetActive(false);
        currentOpenMenu = subMenu;
    }

    // Close submenu screen and show main menu
    public void SubMenuClose(GameObject subMenu)
    {
        subMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentOpenMenu = mainMenu;
    }

    // Change volume level
    public void SetVolume(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        AudioListener.volume = adjustedLevel;
        PlayerPrefs.SetFloat(volumeLevelKey, adjustedLevel);
    }

    // Change brightness level
    public void SetBrightness(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        RenderSettings.ambientLight = new Color(adjustedLevel, adjustedLevel, adjustedLevel);
        PlayerPrefs.SetFloat(brightnessLevelKey, adjustedLevel);
    }

    // Saves the current game and allows it to be loadable when continue button in main menu is pressed
    public void SaveGame()
    {
        // High target goal, may not be implemented
    }
}
