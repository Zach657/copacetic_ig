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

	//Used to update the inventory with the number of brains collected
	[SerializeField] private GameObject inventoryBrainsCollectedObject;

	private int TOTALBRAINS = 10;
    
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
		if (numBrains == TOTALBRAINS) {
			victory ();
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
	public void notebookCollected(){
		
    }
	// Called when the player beats the level
	private void victory(){
		
	}
}
