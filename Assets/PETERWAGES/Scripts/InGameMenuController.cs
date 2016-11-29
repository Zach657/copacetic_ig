using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class InGameMenuController : MenuController
{
    [SerializeField]
    private GameObject playerCharacter;
    // Time variables to allow pausing of game
    private float fixedDeltaTimeOriginal;
    private float timeScaleOriginal;
    private float pauseGameValue = 0.0f;

    // Specific setups for in-game menu only
    void Start () {
        fixedDeltaTimeOriginal = Time.fixedDeltaTime;
        timeScaleOriginal = Time.timeScale;
        mainCamera = Camera.main;
        blurEffectMainCamera = mainCamera.GetComponent<BlurOptimized>();
    }
	
	// Update is called once per frame
	void Update () {
        // check for attempts to open / close in-game menu
        OpenCloseInGameMenuOnEscapeKeyDown();
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

    // Resumes the current game
    public void ResumeGame()
    {
        currentOpenMenu.SetActive(false);
        Time.timeScale = timeScaleOriginal;
        Time.fixedDeltaTime = fixedDeltaTimeOriginal;
        TogglePause(false);
    }

    // Pauses the current game
    public void PauseGame()
    {
        mainMenu.SetActive(true);
        currentOpenMenu = mainMenu;
        Time.timeScale = pauseGameValue;
        Time.fixedDeltaTime = pauseGameValue;
        TogglePause(true);
    }

    // Controls enabling/deactivating camera movement, blur effect, cursor visability
    public void TogglePause(bool toggle)
    {
        menuClosed = !toggle;
        SetCameraMovement(!toggle);
        blurEffectMainCamera.enabled = toggle;
        Cursor.visible = toggle;
    }

    // Disables mouse movement of player camera when menu is open for both the X-controller on the player character and the Y-controller on the main camera
    public void SetCameraMovement(bool active)
    {
            playerCharacter.GetComponent<MouseLook>().enabled = active;
            mainCamera.GetComponent<MouseLook>().enabled = active;
    }
}
