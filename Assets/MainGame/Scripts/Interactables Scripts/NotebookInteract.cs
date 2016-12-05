using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/** 
 * Copyright (C) 2016 - James Greenwell
 **/

public class NotebookInteract : MonoBehaviour {

	//A reference to the player object
	[SerializeField] private Transform playerTransform;

	//Sets the distance the player must be from the brain to interact with it
	[SerializeField] private float minDistance;

	//Sets the audio clip that plays when the player collect the brain
	[SerializeField] private AudioClip ac;

	//Connects the notebook to its inventory counterpart
	[SerializeField] private GameObject notebookObject;

	// Use this for initialization
	void Start () {

	}

	//Constantly checking whether or not the player is interacting with the brain
	void Update () {
		if(playerIsNear() && Input.GetKeyDown("e")){
			playSound();
            notebookObject.GetComponent<Button>().enabled = true;
            Text notebookText = notebookObject.GetComponent<Text>();
            string text = "*" + notebookText.text;
            notebookText.text = text;
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
