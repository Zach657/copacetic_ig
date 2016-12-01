﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class SceneController : Controller {

    [SerializeField]
    private MenuController deceasedController;
    [SerializeField]
	private MenuController winController;
	[SerializeField] private string brainsCollectedKey;
	private const int TOTALBRAINS = 10;

	//Used to update the inventory with the number of brains collected
	[SerializeField] private GameObject inventoryBrainsCollectedObject;

	private List<string> message = new List<string>();

    // Use this for initialization
    void Start () {
        // At the start of each scene, save the checkpoint
        currentScene = SceneManager.GetActiveScene().name;
        SaveGame();

		PlayerPrefs.SetInt (brainsCollectedKey, 0);
		//Adds memory messages to message List
		message.Add("My… my name … my name is Killian.");
		message.Add("I have a family… a … a wife and two kids. Where are they?");
		message.Add("There was an accident I think … Jesus, my head is killing me. Where the hell am I!?");
		message.Add("I remember now.  I was in the car and then … then what?");
		message.Add("Someone’s in the road. Someones in the road and they aren’t moving they’re just standing there. Move! I can’t stop! The car’s not stopping!");
		message.Add("That… that was me.");
		message.Add("Did I die?");
		message.Add("This can’t be the end.");
		message.Add("I won’t let it.");
		message.Add("I won’t let him keep me here. I can feel my heartbeat. I’m not ready.");

	}
		
	// Update is called once per frame
	void Update () {

	}

	// Called when the player collects a brain
	public void brainCollected(){
		int numBrains = PlayerPrefs.GetInt(brainsCollectedKey);
		if(numBrains != null){
			numBrains = numBrains + 1;
		}
		else{
			numBrains = 1;
		}
		if (numBrains == TOTALBRAINS) {
			TriggerWin ();
		}
		PlayerPrefs.SetInt(brainsCollectedKey, numBrains);

		//Displays Memory to Screen
		recallMemory(numBrains);

	}

	//Displays a player memory to the screen upon brain collection
	private void recallMemory(int memNum){
		//headsUpDisplay.enabled = true;
		DisplayNotification (message[memNum-1]);
	}

	// Called when the player collects a notebook
	public void notebookCollected(){

	}

    // Triggers death
    public void TriggerDeath()
    {
        playerAlive = false;
        deceasedController.PauseGame();
    }

    // Trigger player win
    public void TriggerWin()
    {
        winController.PauseGame();
        PlayerPrefs.SetInt(gameInProgressKey, intFalse);
    }
}
