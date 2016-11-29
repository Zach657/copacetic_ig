using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - James Greenwell
 **/

public class BrainInteract : MonoBehaviour {
    
    //A reference to the game object 
    [SerializeField] private GameObject gameStatusController;
    
    //A reference to the player object
    [SerializeField] private Transform playerTransform;
    
    //Sets the distance the player must be from the brain to interact with it
    [SerializeField] private float minDistance;
    
    //Sets the audio clip that plays when the player collect the brain
    [SerializeField] private AudioClip ac;
    
    //Specifies the method to call in the gameStatusController when the brain is collected
    [SerializeField] private string updateBrainMessage;
    
	// Use this for initialization
	void Start () {
	   
	}
	
	//Constantly checking whether or not the player is interacting with the brain
	void Update () {
	   if(playerIsNear() && Input.GetKeyDown("e")){
           playSound();
           gameStatusController.SendMessage (updateBrainMessage);
           Destroy(this.gameObject);
       }
	}
    
    //Plays a sound when the player collects the brain
    private void playSound(){
        AudioSource.PlayClipAtPoint(ac, playerTransform.position);
    }
    
    private bool playerIsNear(){
        float xDistance = Mathf.Abs(gameObject.transform.position.x-playerTransform.position.x);
        float zDistance = Mathf.Abs(gameObject.transform.position.z-playerTransform.position.z);
        if(Mathf.Sqrt((xDistance*xDistance) + (zDistance*zDistance)) <= minDistance){
            return true;
        }
        return false;
    }
}
