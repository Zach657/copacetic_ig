using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class InGameMenuControllerModified : MonoBehaviour {

	[SerializeField]
	private GameObject inGameMenu;
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

	//Used to reference Player look movement scripts
	[SerializeField]
	private GameObject playerObject;
	[SerializeField]
	private GameObject mainCameraObject;

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
		//check for attempts to open/close in-game menu
		OpenCloseInGameMenuOnEscapeKeyDown();
	}

	// Setups the menu screen and game settings
	private void Setup()
	{
		// Defaults brightness to 0%
		brightnessLevel = PlayerPrefs.GetFloat(brightnessLevelKey, brightnessLevel);
		brightnessSlider.value = brightnessLevel * wholeNumberSettingsMultiplier;
		// Defaults volume to 50%
		volumeLevel = PlayerPrefs.GetFloat(volumeLevelKey, volumeLevel);
		volumeSlider.value = volumeLevel * wholeNumberSettingsMultiplier;
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
		inGameMenu.SetActive(false);
		currentOpenMenu = subMenu;
	}

	// Close submenu screen and show main menu
	public void SubMenuClose(GameObject subMenu)
	{
		subMenu.SetActive(false);
		inGameMenu.SetActive(true);
		currentOpenMenu = inGameMenu;
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
		playerObject.GetComponent<MouseLook>().enabled = true;
		mainCameraObject.GetComponent<MouseLook>().enabled = true;
	}

	// Pauses the current game
	public void PauseGame()
	{
		inGameMenu.SetActive(true);
		currentOpenMenu = inGameMenu;
		Time.timeScale = pauseGameValue;
		Time.fixedDeltaTime = pauseGameValue;
		menuClosed = false;
		blurEffectMainCamera.enabled = true;
		playerObject.GetComponent<MouseLook>().enabled = false;
		mainCameraObject.GetComponent<MouseLook>().enabled = false;
	}
}

