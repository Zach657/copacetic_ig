using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MenuController : MonoBehaviour {
    [SerializeField]
    private bool inGameMenu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject newGameButton;
    [SerializeField]
    private GameObject continueGameButton;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider brightnessSlider;
    private Vector3 centerNewGameNoContinue = new Vector3(0f, 50f, 0f);
    private bool gameInProgress = false;

    // Time variables to allow pausing of game
    private float fixedDeltaTimeOriginal;
    private float timeScaleOriginal;
    private float pauseGameValue = 0.0f;

    // Multiplier for whole number volume and brightness settings to float value
    private float floatSettingsMultiplier = .1f;
    // Multiplier for float number volume and brightness settings to whole number value
    private float wholeNumberSettingsMultiplier = 10f;
    
    // Boolean for checking if in-game menu is closed
    private bool menuClosed = true;

    // Currrent open menu
    private GameObject currentOpenMenu;

    private Camera mainCamera;
    private BlurOptimized blurEffectMainCamera;


    // Player pref keys
    private string keyBase = "CopaceticGamesCo-Noises-Game-";
    private string gameInProgressKey;
    private string brightnessLevelKey;
    private string volumeLevelKey;

    // Player pref settings
    private float brightnessLevel = 0.0f;
    private float volumeLevel = .5f;

    // Menu shared setup
    void Start()
    {
        gameInProgressKey = keyBase + "GameInProgressKey";
        brightnessLevelKey = keyBase + "BrightnessLevelKey";
        volumeLevelKey = keyBase + "VolumeLevelKey";
        fixedDeltaTimeOriginal = Time.fixedDeltaTime;
        timeScaleOriginal = Time.timeScale;
        mainCamera = Camera.main;
        blurEffectMainCamera = mainCamera.GetComponent<BlurOptimized>();
        Setup();
    }

    void Update()
    {
        // If not in the main menu, check for attempts to open/close in-game menu
        if (inGameMenu)
        {
            OpenCloseInGameMenuOnEscapeKeyDown();
        }
    }

    // Setups the menu screen and game settings
    private void Setup()
    {
        // Setup for main menu only
        if (!inGameMenu)
        {
            MainMenuSetupSpecifics();
        }
        // Defaults brightness to 0%
        brightnessLevel = PlayerPrefs.GetFloat(brightnessLevelKey, brightnessLevel);
        brightnessSlider.value = brightnessLevel * wholeNumberSettingsMultiplier;
        // Defaults volume to 50%
        volumeLevel = PlayerPrefs.GetFloat(volumeLevelKey, volumeLevel);
        volumeSlider.value = volumeLevel * wholeNumberSettingsMultiplier;
    }

    // Specific setups for main menu only
    private void MainMenuSetupSpecifics()
    {
        // 1 is true, 0 is false, since PlayerPrefs does not support booleans natively
        gameInProgress = PlayerPrefs.GetInt(gameInProgressKey, 0) == 1 ? true : false;
        if (!gameInProgress)
        {
            newGameButton.GetComponent<RectTransform>().anchoredPosition3D = centerNewGameNoContinue;
            Destroy(continueGameButton);
        }
    }

    // Opens the in game menu on escape key press
    private void OpenCloseInGameMenuOnEscapeKeyDown()
    {
        // On escape button press, pause or resume the game
        if (Input.GetKeyDown(KeyCode.Escape) && menuClosed)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !menuClosed)
        {
            ResumeGame();
        }
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

    // Resumes the current game
    public void ResumeGame()
    {
        currentOpenMenu.SetActive(false);
        Time.timeScale = timeScaleOriginal;
        Time.fixedDeltaTime = fixedDeltaTimeOriginal;
        menuClosed = true;
        blurEffectMainCamera.enabled = false;
    }

    // Pauses the current game
    public void PauseGame()
    {
        mainMenu.SetActive(true);
        currentOpenMenu = mainMenu;
        Time.timeScale = pauseGameValue;
        Time.fixedDeltaTime = pauseGameValue;
         menuClosed = false;
        blurEffectMainCamera.enabled = true;
    }

    // Saves the current game and allows it to be loadable when continue button in main menu is pressed
    public void SaveGame()
    {
        // High target goal, may not be implemented
    }
}
