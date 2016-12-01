using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class MenuController : Controller {
    public GameObject mainMenu;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider brightnessSlider;

    [SerializeField]
    private GameObject playerCharacter;

    // Multiplier for whole number volume and brightness settings to float value
    private float floatSettingsMultiplier = .1f;
    // Multiplier for float number volume and brightness settings to whole number value
    private float wholeNumberSettingsMultiplier = 10f;

    // Currrent open menu
    private GameObject currentOpenMenu;

    // Camera objects
    private Camera mainCamera;
    private BlurOptimized blurEffectMainCamera;

    // Player pref settings
    private float brightnessLevel = 0.0f;
    private float volumeLevel = .5f;

    // Time variables to allow pausing of game
    private float fixedDeltaTimeOriginal;
    private float timeScaleOriginal;
    private float pauseGameValue = 0.0f;

    // Menu setup
    void Start()
    {
        fixedDeltaTimeOriginal = Time.fixedDeltaTime;
        timeScaleOriginal = Time.timeScale;
        mainCamera = Camera.main;
        blurEffectMainCamera = mainCamera.GetComponent<BlurOptimized>();

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
        #if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
        #else
               Application.Quit();
        #endif
    }

    // Continues saved game / most recent player checkpoint
    public void ContinueSavedGame()
    {
        LoadGameScene(PlayerPrefs.GetString(savedGameSceneKey, currentScene));
    }

    // Activates submenu screen and hide main menu
    private void SubMenuOpen(GameObject subMenu)
    {
        subMenu.SetActive(true);
        mainMenu.SetActive(false);
        currentOpenMenu = subMenu;
    }

    // Close submenu screen and show main menu
    private void SubMenuClose(GameObject subMenu)
    {
        subMenu.SetActive(false);
        mainMenu.SetActive(true);
        currentOpenMenu = mainMenu;
    }

    // Change volume level
    private void SetVolume(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        AudioListener.volume = adjustedLevel;
        PlayerPrefs.SetFloat(volumeLevelKey, adjustedLevel);
    }

    // Change brightness level
    private void SetBrightness(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        RenderSettings.ambientLight = new Color(adjustedLevel, adjustedLevel, adjustedLevel);
        PlayerPrefs.SetFloat(brightnessLevelKey, adjustedLevel);
    }

    // Controls enabling/deactivating camera movement, blur effect, cursor visability
    private void TogglePause(bool toggle)
    {
        SetCameraMovement(!toggle);
        blurEffectMainCamera.enabled = toggle;
        Cursor.visible = toggle;
    }

    // Disables mouse movement of player camera when menu is open for both the X-controller on the player character and the Y-controller on the main camera
    private void SetCameraMovement(bool active)
    {
        playerCharacter.GetComponent<MouseLook>().enabled = active;
        mainCamera.GetComponent<MouseLook>().enabled = active;
    }

    // Resumes the current game
    public void ResumeGame()
    {
        currentOpenMenu.SetActive(false);
        Time.timeScale = timeScaleOriginal;
        Time.fixedDeltaTime = fixedDeltaTimeOriginal;
        TogglePause(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Pauses the current game
    public void PauseGame()
    {
        mainMenu.SetActive(true);
        currentOpenMenu = mainMenu;
        Time.timeScale = pauseGameValue;
        Time.fixedDeltaTime = pauseGameValue;
        TogglePause(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

}
