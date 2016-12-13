using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - James Greenwell
 **/

public class BrainInteract : MonoBehaviour {      
    //Sets the audio clip that plays when the player collect the brain
    [SerializeField] private AudioClip ac;
	
	//Constantly checking whether or not the player is interacting with the brain
	void Update () {
	   if(SceneController.playerIsNear(this.gameObject) && Input.GetKeyDown("e")){
           playSound();
            Utilities.sceneController.BrainCollected();
           Destroy(this.gameObject);
       }
	}
    
    //Plays a sound when the player collects the brain
    private void playSound(){
        AudioSource.PlayClipAtPoint(ac, Utilities.playerCharacter.transform.position);
    }
    
}
