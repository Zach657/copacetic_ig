using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - James Greenwell
 **/

public class DoorInteract : MonoBehaviour {
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
        isMoving = false;
        if(isOpen){
            currentAngle = openAngle;
        }
        else{
            currentAngle = closeAngle;
        }
        
    }
    
    void FixedUpdate(){
        if(playerIsNear() && Input.GetKeyDown("e") && !isMoving && !isLocked){
            isMoving = true;
            if(isOpen){
                targetAngle = closeAngle;
                playCloseSound();
                isOpen = false;
            }
            else{
                targetAngle = openAngle;
                playOpenSound();
                isOpen = true;
            }
        }
        else if(isMoving && !isLocked){
            moveDoor(targetAngle);
        } else if (!isLocked)
        {

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
}

