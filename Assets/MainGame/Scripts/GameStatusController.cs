using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - James Greenwell
 **/

public class GameStatusController : MonoBehaviour {

    [SerializeField] private GameObject GameUI;
    
    [SerializeField] private string brainsCollectedKey;
    
	//Used to notify user of various occurences
	[SerializeField] private GameObject headsUpDisplay;

	//Used to allow access to collected journals through player inventory
	[SerializeField] private GameObject inventoryObject;

	//Sets the display time of each message
	private int displayForMillis = 5000;
    
	// Use this for initialization
	void Start () {
		//TODO implement checkpoint feature
		PlayerPrefs.SetInt (brainsCollectedKey, 0);
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
        PlayerPrefs.SetInt(brainsCollectedKey, numBrains);
		//Displays Memory to Screen
        recallMemory(numBrains);
    }
    
    //Displays a player memory to the screen upon brain collection
    private void recallMemory(int memNum){
		//headsUpDisplay.enabled = true;
    }
    
    // Called when the player collects a notebook
	public void notebookCollected(int notebookNum){
		
    }
}
