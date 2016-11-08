using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
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

    // Multiplier for whole number volume and brightness settings to float value
    private float floatSettingsMultiplier = .1f;
    // Multiplier for float number volume and brightness settings to whole number value
    private float wholeNumberSettingsMultiplier = 10f;


    // Player pref keys
    private string keyBase = "CopaceticGamesCo-Noises-Game-";
    private string gameInProgressKey;
    private string brightnessLevelKey;
    private string volumeLevelKey;

    // Player pref settings
    private float brightnessLevel = 0.0f;
    private float volumeLevel = .5f;

    // Menu setup
    void Start()
    {
        gameInProgressKey = keyBase + "GameInProgressKey";
        brightnessLevelKey = keyBase + "BrightnessLevelKey";
        volumeLevelKey = keyBase + "VolumeLevelKey";
        Setup();
    }

    // Setups the menu screen and game settings
    private void Setup()
    {
        // 1 is true, 0 is false, since PlayerPrefs does not support booleans natively
        gameInProgress = PlayerPrefs.GetInt(gameInProgressKey, 0) == 1 ? true : false;
        if (!gameInProgress)
        {
            newGameButton.GetComponent<RectTransform>().anchoredPosition3D = centerNewGameNoContinue;
            Destroy(continueGameButton);
        }
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

    // Starts a new game
    public void NewGame()
    {
        SceneManager.LoadScene("NoisesIntroductionScene");
    }

    // Continues current game
    public void ContinueGame()
    {

    }

    // Activates submenu screen and hide main menu
    public void SubMenuOpen(GameObject subMenu)
    {
        subMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    // Close submenu screen and show main menu
    public void SubMenuClose(GameObject subMenu)
    {
        subMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Change volume level
    public void setVolume(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        AudioListener.volume = adjustedLevel;
        PlayerPrefs.SetFloat(volumeLevelKey, adjustedLevel);
    }

    // Change brightness level
    public void setBrightness(float level)
    {
        float adjustedLevel = level * floatSettingsMultiplier;
        RenderSettings.ambientLight = new Color(adjustedLevel, adjustedLevel, adjustedLevel);
        PlayerPrefs.SetFloat(brightnessLevelKey, adjustedLevel);
    }



}
