using UnityEngine;
using System.Collections;
using System;

/** 
 * Copyright (C) 2016 - James Greenwell & Peter Wages
 **/
 // All James Greenwell, except where noted
public class DoorInteract : MonoBehaviour, UnlockableObject {
    //Sets the open and close angles for the given door
    [SerializeField] private float openAngle;
    [SerializeField] private float closeAngle;
    
    //References to the open and close door sound clips
    [SerializeField] private AudioClip open;
    [SerializeField] private AudioClip close;
    
    //A reference to the player object
    [SerializeField] private Transform playerTransform;
    
    //Sets the distance the player must be from the door to interact with it
    [SerializeField] private float minDistance;
    
    //boolean for whether or not the door is open
    [SerializeField] private bool isOpen;

    // Peter Wages
    [SerializeField]
    private MenuController menuController;
    [SerializeField]
    private GameObject keypad;
    //boolean for whether or not the door is locked
    [SerializeField]
    private bool isLocked;
    // Peter Wages

    //boolean for whether or not the door is moving
    private bool isMoving;
    
    //used to determine which angle the door should move towards
    private float targetAngle;
    
    //used to track the current angle of the door (handles issues with door rotation)
    private float currentAngle;

	private DoorState currentState;
    
    void Start(){
        menuController = GameObject.FindObjectOfType<InGameMenuController>();
        isMoving = false;
        if(isOpen){
            currentAngle = openAngle;
			currentState = new UnlockedState (this);
        }
        else{
            currentAngle = closeAngle;
			currentState = new LockedState (this);
        }
        
    }
    
    void FixedUpdate(){
        if (playerIsNear() && Input.GetKeyDown("e") && !isMoving)
        {
			currentState.FixedUpdate();
        }
        else if (isMoving && !isLocked)
        {
            MoveDoor(targetAngle);
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
    private void MoveDoor(float angle){
        if(Mathf.Abs(currentAngle-angle) < 4){
            isMoving = false;
        }
        else if(angle < currentAngle){
            this.transform.Rotate(0,-4,0);
            currentAngle = currentAngle - 4;
        }
        else{
            this.transform.Rotate(0,4,0);
            currentAngle = currentAngle + 4;
        }
    }

    //Peter Wages
    // Unlocks the door from the numberpad script
    public void Unlock()
    {
        isLocked = false;
		currentState = new UnlockedState (this);
        ToggleDoor(openAngle, open, true);
		currentState.MoveDoor();
    }
    // Peter Wages

    public void ToggleDoor(float angle, AudioClip clip, bool toggle)
    {
        isMoving = true;
        targetAngle = angle;
        menuController.PlaySoundAtPoint(clip, playerTransform.position);
        isOpen = toggle;
    }

	// Nathan Pool
	class LockedState : DoorState {
		DoorInteract interaction;
		public LockedState(DoorInteract interaction) {
			this.interaction = interaction;
		}

		public void MoveDoor() {
			Debug.Log("Locked");
			interaction.menuController.PauseGame(interaction.keypad);
			GameObject.FindObjectOfType<NumpadEntryController>().thisUnlockable = interaction.gameObject;
			interaction.menuController.SetKeypadPuzzle(interaction.gameObject.GetComponent<Puzzle>());
		}

		public void FixedUpdate() {
			this.MoveDoor ();
		}

		public void PlaySound() {
			AudioSource.PlayClipAtPoint(interaction.close, interaction.transform.position);
		}
	}

	//Nathan Pool
	class UnlockedState : DoorState {
		DoorInteract interaction;
		public UnlockedState(DoorInteract interaction) {
			this.interaction = interaction;
		}

		public void MoveDoor() {
			if (interaction.isOpen)
			{
				interaction.ToggleDoor(interaction.closeAngle, interaction.close, false);
			}
			else
			{
				interaction.ToggleDoor(interaction.openAngle, interaction.open, true);
			}
		}

		public void PlaySound() {
			AudioSource.PlayClipAtPoint(interaction.open, interaction.transform.position);
		}

		public void FixedUpdate() {
			this.MoveDoor ();
		}
	}

}

