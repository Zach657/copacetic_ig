using UnityEngine;
using System.Collections;
using System;

/** 
 * Copyright (C) 2016 - James Greenwell, Nathan Pool & Peter Wages
 **/
 // James Greenwell, except where noted, refactored into states by Nathan Pool
public class DoorInteract : MonoBehaviour, UnlockableObject {
    //Sets the open and close angles for the given door
    [SerializeField] private float openAngle;
    [SerializeField] private float closeAngle;
    
    //References to the open and close door sound clips
    [SerializeField] private AudioClip open;
    [SerializeField] private AudioClip close;
    
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
        if(!isLocked){
			currentState = new UnlockedState (this);
        }
        else{
			currentState = new LockedState (this);
        }
        if (isOpen)
        {
            currentAngle = openAngle;
        } else
        {
            currentAngle = closeAngle;
        }
        
    }
    
    void FixedUpdate(){
        if (SceneController.playerIsNear(this.gameObject) && Input.GetKeyDown("e") && !isMoving)
        {
			currentState.MoveDoor();
        }
        else if (isMoving && !isLocked)
        {
            MoveDoor(targetAngle);
        }
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
    }
    // Peter Wages

    public void ToggleDoor(float angle, AudioClip clip, bool toggle)
    {
        isMoving = true;
        targetAngle = angle;
        SceneController.PlaySoundAtPoint(clip, Utilities.playerCharacter.transform.position);
        isOpen = toggle;
    }

	// Nathan Pool
	class LockedState : DoorState {
		DoorInteract interaction;
		public LockedState(DoorInteract interaction) {
			this.interaction = interaction;
		}

		public void MoveDoor() {
			Utilities.sceneController.PauseGame(interaction.keypad);
			Utilities.numpadController.thisUnlockable = interaction.gameObject;
            SceneController.SetKeypadPuzzle(interaction.gameObject.GetComponent<Puzzle>());
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
	}

}

