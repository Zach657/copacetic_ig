using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
 [RequireComponent(typeof(SceneController))]
public class Utilities: MonoBehaviour {
    // Player pref keys
    private const string KEYBASE = "CopaceticGamesCo-Noises-Game-";
    public const string GAMEINPROGRESSKEY = KEYBASE + "GameInProgressKey";
    public const string BRIGHTNESSLEVELKEY = KEYBASE + "BrightnessLevelKey";
    public const string VOLUMELEVELKEY = KEYBASE + "VolumeLevelKey";
    public const string SAVEDGAMELEVELKEY = KEYBASE + "SavedGameSceneKey";

    // Level loading strings
    public const string LEVELONE = "environment-asylum";
    public const string LEVELTWO = "environment-outside";

    // Project-wide integer based booleans for use in Player Prefs ONLY
    public const int INTTRUE = 1;
    public const int INTFALSE = 0;

    // CrossFadeAlpha refuses to work if Alpha is set to 0, so setting to 1
    public const float INVISIBLEALPHA = 1f;
    public const float VISIBLEALPHA = 255f;
    public const float VISIBLEBACKGROUNDALPHA = 150f;
    public const float FADEDURATION = 1.25f;
    public const float NOTIFICATIONDURATION = 8;

    // Controllers for non-static method access
    public static SceneController sceneController;
    public static MenuController menuController;

    // Time variables to allow pausing of game
    public const float PAUSEGAMEVALUE = 0.0f;
    // Time variables to allow pausing of game
    public const float TIMESCALEORIGINAL = 1;

    //Sets the distance the player must be from the brain to interact with it
    public const float MINIMUMDISTANCE = 1.5f;

    // Scene information
    public static string currentScene;

    // Menu system
    public static GameObject mainMenu;
    // Currrent open menu
    public static GameObject currentOpenMenu;
    // Boolean for checking if in-game menu is closed
    public static bool menuClosed = true;

    // Player information
    public static GameObject playerCharacter;
    public static bool playerAlive = true;
    public static bool gameIsResumable = true;

    // Notification system
    public static GameObject notificationWindowSystem;
    public static Text notificationMessage;

    // Minimap and compass systems
    public static GameObject minimap;
    public static GameObject compass;

    // Text to show current number of brain memories found
    public static Text memoriesCollectedText;
    
    // Numpad system
    public static NumpadEntryController numpadController;
    public static GameObject numpadObject;
    public static Text numpadEntryTextField;

    // Game results menus
    public static GameObject lostMenu;
    public static GameObject winMenu;

    // Settings systems
    public static Slider volumeSlider;
    public static Slider brightnessSlider;

    // Camera objects
    public static Camera mainCamera;
    public static BlurOptimized blurEffectMainCamera;


    // Serialized variables because Unity cannot set static variables in editor, but also cannot find inactive objects using GameObject.Find().
    [SerializeField]
    private MenuController menuControllerSerialized;
    [SerializeField]
    private NumpadEntryController numpadControllerSerialized;
    [SerializeField]
    private GameObject mainMenuSerialized;
    [SerializeField]
    private GameObject notificationWindowSystemSerialized;
    [SerializeField]
    private GameObject minimapSerialized;
    [SerializeField]
    private GameObject compassSerialized;
    [SerializeField]
    private GameObject numpadObjectSerialized;
    [SerializeField]
    private Text numpadEntryTextFieldSerialized;
    [SerializeField]
    private Text memoriesCollectedTextSerialized;
    [SerializeField]
    private GameObject lostMenuSerialized;
    [SerializeField]
    private GameObject winMenuSerialized;
    [SerializeField]
    private Camera mainCameraSerialized;

    public void Start()
    {
        sceneController = GetComponent<SceneController>();
        menuController = menuControllerSerialized;
        numpadController = numpadControllerSerialized;
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        mainMenu = mainMenuSerialized;
        notificationWindowSystem = notificationWindowSystemSerialized;
        notificationMessage = notificationWindowSystem.GetComponentInChildren<Text>();
        minimap = minimapSerialized;
        compass = compassSerialized;
        memoriesCollectedText = memoriesCollectedTextSerialized;
        numpadObject = numpadObjectSerialized;
        numpadEntryTextField = numpadEntryTextFieldSerialized;
        lostMenu = lostMenuSerialized;
        winMenu = winMenuSerialized;
        mainCamera = Camera.main;
        blurEffectMainCamera = mainCamera.GetComponent<BlurOptimized>();
    }
}
