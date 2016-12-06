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
    
    void Start(){
        menuController = GameObject.FindObjectOfType<InGameMenuController>();
        isMoving = false;
        if(isOpen){
            currentAngle = openAngle;
        }
        else{
            currentAngle = closeAngle;
        }
        
    }
    
    void FixedUpdate(){
        if (playerIsNear() && Input.GetKeyDown("e") && !isMoving)
        {
            if (isLocked)
            // Peter Wages
            {
                Debug.Log("Locked");
                menuController.PauseGame(keypad);
                GameObject.FindObjectOfType<NumpadEntryController>().thisUnlockable = this.gameObject;
                // Peter Wages
            }
            else
            {

                isMoving = true;
                if (isOpen)
                {
                    targetAngle = closeAngle;
                    playCloseSound();
                    isOpen = false;
                }
                else
                {
                    targetAngle = openAngle;
                    playOpenSound();
                    isOpen = true;
                }
            }
        }
        else if (isMoving && !isLocked)
        {
            moveDoor(targetAngle);
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
    private void moveDoor(float angle){
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
    
    //plays door opening sound
    private void playOpenSound(){
        AudioSource.PlayClipAtPoint(open, this.transform.position);
    }
    //plays door closing sound
    private void playCloseSound(){
        AudioSource.PlayClipAtPoint(close, this.transform.position);
    }

    //Peter Wages
    // Unlocks the door
    public void Unlock()
    {
        isLocked = false;
    }
    // Peter Wages
}
