using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class SceneController : Controller {

    [SerializeField]
    private MenuController deceasedController;
    [SerializeField]
    private MenuController finishController;

    // Use this for initialization
    void Start () {
        // At the start of each scene, save the checkpoint
        currentScene = SceneManager.GetActiveScene().name;
        SaveGame();
	}

    public void TriggerDeath()
    {
        playerAlive = false;
        deceasedController.PauseGame();
    }

    public void TriggerWin()
    {
        finishController.PauseGame();
    }
}
