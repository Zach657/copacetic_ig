using UnityEngine;
using System.Collections;
using System;

/** 
 * Copyright (C) 2016 - James Greenwell, Nathan Pool & Peter Wages
 **/
 // James Greenwell, except where noted, then refactored into states by Nathan Pool
public class DoorInteract : MonoBehaviour, UnlockableObject {
    //Sets the open and close angles for the given door
    [SerializeField] private float openAngle;
    [SerializeField] private float closeAngle;
    //boolean for whether or not the door is moving
    private bool isMoving;
    //used to determine which angle the door should move towards
    private float targetAngle;
    //used to track the current angle of the door (handles issues with door rotation)
    private float currentAngle;

    //boolean for whether or not the door is open
    [SerializeField] private bool isOpen;
    // Peter Wages
    //boolean for whether or not the door is locked
    [SerializeField]
    private bool isLocked;
    // Peter Wages

	private DoorState currentState;
    
    // Sets the starting state and current angle
    void Start(){
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
    
    // Constantly checks if player is nearby and attempting to open the door
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
        ToggleDoor(openAngle, Utilities.openDoorSound, true);
    }
    // Peter Wages

        // Toggles the door from open to close and plays the audio clip
    public void ToggleDoor(float angle, AudioClip clip, bool toggle)
    {
        isMoving = true;
        targetAngle = angle;
        SceneController.PlaySoundAtPoint(clip, Utilities.playerCharacter.transform.position);
        isOpen = toggle;
    }

	// Nathan Pool
    // State for when locked
	class LockedState : DoorState {
		DoorInteract interaction;
		public LockedState(DoorInteract interaction) {
			this.interaction = interaction;
		}

        // Since locked, open the numpad to solve puzzle and unlock door
		public void MoveDoor() {
			Utilities.sceneController.PauseGame(Utilities.numpadObject);
			Utilities.numpadController.thisUnlockable = interaction.gameObject;
            SceneController.SetKeypadPuzzle(interaction.gameObject.GetComponent<Puzzle>());
		}
	}

	//Nathan Pool
    // Unlocked state
	class UnlockedState : DoorState {
		DoorInteract interaction;
		public UnlockedState(DoorInteract interaction) {
			this.interaction = interaction;
		}

        // Toggles the door
		public void MoveDoor() {
			if (interaction.isOpen)
			{
				interaction.ToggleDoor(interaction.closeAngle, Utilities.closeDoorSound, false);
			}
			else
			{
				interaction.ToggleDoor(interaction.openAngle, Utilities.openDoorSound, true);
			}
		}
	}

}

