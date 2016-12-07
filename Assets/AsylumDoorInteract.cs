using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/** 
 * Copyright (C) 2016 - Nathan Pool and James Greenwell
 **/

public class AsylumDoorInteract : MonoBehaviour {
	private const int OPEN_ANGLE = -60;

	//Sets the open and close angles for the given door
	[SerializeField] private float openAngle;
	[SerializeField] private float closeAngle;

	//References to the open and close door sound clips
	[SerializeField] private AudioClip open;
	[SerializeField] private AudioClip close;

	//A reference to the game object 
	[SerializeField] private GameObject gameStatusController;

	//A reference to the player object
	[SerializeField] private Transform playerTransform;

	//Sets the distance the player must be from the door to interact with it
	[SerializeField] private float minDistance;

	//boolean for whether or not the door is open
	[SerializeField] private bool isOpen;

	// code solved
	private bool codeSolved;

	//boolean for whether or not the door is moving
	private bool isMoving;

	//used to determine which angle the door should move towards
	private float targetAngle;

	//used to track the current angle of the door (handles issues with door rotation)
	private float currentAngle;

	GameObject keypad;

	// State
	private DoorState currentState;

	void Start(){
		keypad = GameObject.Find ("CodeInput");
		keypad.SetActive (false);
		codeSolved = false;
		currentState = new LockedState (this);
		isMoving = false;
//		if(isOpen){
//			currentAngle = openAngle;
//		}
//		else{
//			currentAngle = closeAngle;
//		}

	}

	void FixedUpdate(){
		if(playerIsNear() && Input.GetKeyDown("e") && !isMoving){
//			isMoving = true;
//			if(isOpen){
//				targetAngle = closeAngle;
//				//playCloseSound();
//				isOpen = false;
//			}
//			else{
//				targetAngle = openAngle;
//				//playOpenSound();
//				isOpen = true;
//			}
//		}
//		else if(isMoving){
//			MoveDoor();
//		}
		currentState.FixedUpdate ();
//		targetAngle = openAngle;
//		currentState.playSound ();
		}
		if (codeSolved) {
			currentState = new UnlockedState (this);
		}
	}


	private bool playerIsNear(){
		float xDistance = Mathf.Abs(gameObject.transform.position.x-playerTransform.position.x);
		float zDistance = Mathf.Abs(gameObject.transform.position.z-playerTransform.position.z);
		if(Mathf.Sqrt((xDistance*xDistance) + (zDistance*zDistance)) <= minDistance){
			return true;
		}
		return false;
	}

	//moves the door towards the desired angle in increments
	public void MoveDoor(){
//		if(Mathf.Abs(currentAngle-angle) < 4){
//			isMoving = false;
//		}
//		else if(angle < currentAngle){
//			this.transform.Rotate(0,-4,0);
//			currentAngle = currentAngle - 4;
//		}
//		else{
//			this.transform.Rotate(0,4,0);
//			currentAngle = currentAngle + 4;
//		}
		keypad.SetActive(true);
		currentState.MoveDoor();
	}

	public void SetUnlocked() {
		currentState = new UnlockedState (this);
		currentState.MoveDoor ();
	}

	public void WinLevel() {
		StartCoroutine (PuzzleSolved ());
	}

	public IEnumerator PuzzleSolved() {
		yield return new WaitForSeconds(5);
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
			Destroy(o);
		}
		SceneManager.LoadScene ("environment-outside", LoadSceneMode.Additive);
	}

//	//plays door opening sound
//	private void playOpenSound(){
//		AudioSource.PlayClipAtPoint(open, this.transform.position);
//	}
//	//plays door closing sound
//	private void playCloseSound(){
//		AudioSource.PlayClipAtPoint(close, this.transform.position);
//	}

	class LockedState : DoorState {
		AsylumDoorInteract interaction;
		public LockedState(AsylumDoorInteract interaction) {
			this.interaction = interaction;
		}

		public void MoveDoor() {
			interaction.keypad.SetActive (true);
			Cursor.visible = true;
		}

		public void FixedUpdate() {
			this.MoveDoor ();
		}

		public void PlaySound() {
			AudioSource.PlayClipAtPoint(interaction.close, interaction.transform.position);
		}
	}

	class UnlockedState : DoorState {
		AsylumDoorInteract interaction;
		public UnlockedState(AsylumDoorInteract interaction) {
			this.interaction = interaction;
		}

		public void MoveDoor() {
			// interaction.gameStatusController.SendMessage("Thank God, I'm free");
			PlaySound();
			interaction.transform.Rotate(0,OPEN_ANGLE,0);
			interaction.currentAngle = interaction.currentAngle - OPEN_ANGLE;
			interaction.WinLevel ();
		}

		public void PlaySound() {
			AudioSource.PlayClipAtPoint(interaction.open, interaction.transform.position);
		}

		public void FixedUpdate() {
			if (interaction.playerIsNear () && Input.GetKeyDown ("e") && !interaction.isMoving) {
				interaction.targetAngle = interaction.openAngle;
			} else if(interaction.isMoving){
				this.MoveDoor();
			}
		}
			
	}


}

