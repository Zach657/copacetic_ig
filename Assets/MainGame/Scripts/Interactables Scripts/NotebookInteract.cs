using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/** 
 * Copyright (C) 2016 - James Greenwell
 **/

public class NotebookInteract : MonoBehaviour {
	//Sets the audio clip that plays when the player collect the notebook
	[SerializeField] private AudioClip ac;

	//Connects the notebook to its inventory counterpart
	[SerializeField] private GameObject notebookObject;

	// Use this for initialization
	void Start () {

	}

	//Constantly checking whether or not the player is interacting with the notebook
	void Update () {
		if(SceneController.playerIsNear(this.gameObject) && Input.GetKeyDown("e")){
			playSound();
            notebookObject.GetComponent<Button>().enabled = true;
            Text notebookText = notebookObject.GetComponent<Text>();
            string text = "*" + notebookText.text;
            notebookText.text = text;
            Destroy(this.gameObject);
        }
	}

	//Plays a sound when the player collects the notebook
	private void playSound(){
		AudioSource.PlayClipAtPoint(ac, Utilities.playerCharacter.transform.position);
	}
}
