using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
[RequireComponent(typeof(Utilities))]
[RequireComponent(typeof(IntroductionToScene))]
public class SceneController: MonoBehaviour {

	private const int TOTALBRAINS = 10;
    private int brainsCollected = 0;

	private List<string> brainJarMemories = new List<string>();

    // Use this for initialization
    void Start()
    {
        // At the start of each scene, save the checkpoint
        Utilities.currentScene = SceneManager.GetActiveScene().name;
        SaveGame();
        Utilities.playerAlive = true;
        brainsCollected = 0;

        //Adds memory messages to message List
        brainJarMemories.Add("My… my name … my name is Killian?");
        brainJarMemories.Add("I have a family… L-Landry?... and a …  and a kid... Ronan! Where are they?");
        brainJarMemories.Add("There was an accident I think … Jesus, my head is killing me. Where the hell am I!?");
        brainJarMemories.Add("I remember now.  I was in the car and then … then what?");
        brainJarMemories.Add("Someone’s in the road. Someone's in the road and they aren’t moving they’re just standing there. Move! I can’t stop! The car’s not stopping!");
        brainJarMemories.Add("That… that was me.");
        brainJarMemories.Add("Did I die?");
        brainJarMemories.Add("This can’t be the end.");
        brainJarMemories.Add("I won’t let it.");
        brainJarMemories.Add("I won’t let him keep me here. I can feel my heartbeat. I’m not ready.");
    }

    // Called when the player collects a brain
    public void BrainCollected(){
        brainsCollected++;
		if (brainsCollected >= TOTALBRAINS) {
			TriggerWin ();
		}
		Utilities.memoriesCollectedText.text = brainsCollected + "/10";
        DisplayNotification(brainJarMemories[brainsCollected - 1]);

    }

    // Displays a notification at top of screen
    public void DisplayNotification(string message)
    {
        Utilities.notificationMessage.text = message;
        StartCoroutine(DisplayNotificationForLimitedTime());
    }

    // Sets the current puzzle that the keypad entry is trying to solve
    public static void SetKeypadPuzzle(Puzzle puzzle)
    {
        NumpadEntryController.currentPuzzle = puzzle;
        Utilities.numpadController.IntizializeAnswerString();
    }

    // Controls enabling/deactivating camera movement, blur effect, cursor visability
    private static void TogglePause(bool toggle)
    {
        SetCameraMovement(!toggle);
        Utilities.blurEffectMainCamera.enabled = toggle;
        Cursor.visible = toggle;
    }

    // Disables mouse movement of player camera when menu is open for both the X-controller on the player character and the Y-controller on the main camera
    private static void SetCameraMovement(bool active)
    {
        Utilities.playerCharacter.GetComponent<MouseLook>().enabled = active;
        Utilities.mainCamera.GetComponent<MouseLook>().enabled = active;
    }

    // Resumes the current game
    public void ResumeGame()
    {
        if (Utilities.gameIsResumable)
        {
            Utilities.currentOpenMenu.SetActive(false);
            Time.timeScale = Utilities.TIMESCALEORIGINAL;
            TogglePause(false);
            Cursor.lockState = CursorLockMode.Locked;
            Utilities.menuClosed = true;
        }
    }

    // Pauses the current game
    public void PauseGame(GameObject menuToOpen)
    {
        if (!menuToOpen)
        {
            menuToOpen = Utilities.mainMenu;
        }
        menuToOpen.SetActive(true);
        Utilities.currentOpenMenu = menuToOpen;
        Time.timeScale = Utilities.PAUSEGAMEVALUE;
        TogglePause(true);
        Cursor.lockState = CursorLockMode.Confined;
        Utilities.menuClosed = false;
    }

    // Triggers death
    public static void TriggerDeath()
    {
        Utilities.playerAlive = false;
        Utilities.gameIsResumable = false;
        Utilities.sceneController.PauseGame(Utilities.lostMenu);
    }

    // Trigger player win
    public static void TriggerWin()
    {
        Utilities.sceneController.PauseGame(Utilities.winMenu);
        Utilities.gameIsResumable = false;
        PlayerPrefs.SetInt(Utilities.GAMEINPROGRESSKEY, Utilities.INTFALSE);
    }

    // Loads a specific scene
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Saves the current game and allows it to be loadable when continue button in main menu is pressed
    public void SaveGame()
    {
        PlayerPrefs.SetInt(Utilities.GAMEINPROGRESSKEY, Utilities.INTTRUE);
        PlayerPrefs.SetString(Utilities.SAVEDGAMELEVELKEY, Utilities.currentScene);
    }

    // Fades the notification in and then fades it out
    public static IEnumerator DisplayNotificationForLimitedTime()
    {
        Utilities.notificationWindowSystem.GetComponent<Image>().CrossFadeAlpha(Utilities.VISIBLEBACKGROUNDALPHA, Utilities.FADEDURATION, false);
        Utilities.notificationMessage.CrossFadeAlpha(Utilities.VISIBLEALPHA, Utilities.FADEDURATION, false);
        yield return new WaitForSeconds(Utilities.NOTIFICATIONDURATION);
        Utilities.notificationWindowSystem.GetComponent<Image>().CrossFadeAlpha(Utilities.INVISIBLEALPHA, Utilities.FADEDURATION, false);
        Utilities.notificationMessage.CrossFadeAlpha(Utilities.INVISIBLEALPHA, Utilities.FADEDURATION, false);
    }

    public static bool playerIsNear(GameObject comparison)
    {
        float xDistance = Mathf.Abs(comparison.transform.position.x - Utilities.playerCharacter.transform.position.x);
        float zDistance = Mathf.Abs(comparison.transform.position.z - Utilities.playerCharacter.transform.position.z);
        if (Mathf.Sqrt((xDistance * xDistance) + (zDistance * zDistance)) <= Utilities.minmumDistance)
        {
            return true;
        }
        return false;
    }

    // Plays audio clip at a location
    public static void PlaySoundAtPoint(AudioClip audio, Vector3 location)
    {
        if (location == null)
        {
            location = Utilities.playerCharacter.transform.position;
        }
        AudioSource.PlayClipAtPoint(audio, location);
    }

    public static void OpenCloseMenuOnButtonPress(UnityEngine.KeyCode KeyCodeToBePressed, GameObject objectToToggle) {
        if (Input.GetKeyDown(KeyCodeToBePressed) && Utilities.menuClosed && Utilities.playerAlive)
        {
            Utilities.sceneController.PauseGame(objectToToggle);
        }
        else if (Input.GetKeyDown(KeyCodeToBePressed) && !Utilities.menuClosed && Utilities.playerAlive)
        {
            Utilities.sceneController.ResumeGame();
        }
    }
}
