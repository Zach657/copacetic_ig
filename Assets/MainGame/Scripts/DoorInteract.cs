using UnityEngine;
using System.Collections;

public class DoorInteract : MonoBehaviour {
    //Sets the open and close angles for the given door
    [SerializeField] private float openAngle;
    [SerializeField] private float closeAngle;
    
    //A reference to the player object
    [SerializeField] private Transform playerTransform;
    
    //Sets the distance the player must be from the door to interact with it
    [SerializeField] private float minDistance;
    
    //boolean for whether or not the door is open
    [SerializeField] private bool isOpen;
    
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
        if(playerIsNear() && Input.GetKeyDown("e") && !isMoving){
            isMoving = true;
            if(isOpen){
                targetAngle = closeAngle;
                isOpen = false;
            }
            else{
                targetAngle = openAngle;
                isOpen = true;
            }
        }
        else if(isMoving){
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
        if(Mathf.Abs(currentAngle-angle) < 2){
            isMoving = false;
        }
        else if(angle < currentAngle){
            this.transform.Rotate(0,-2,0);
            currentAngle = currentAngle - 2;
        }
        else{
            this.transform.Rotate(0,2,0);
            currentAngle = currentAngle + 2;
        }
    }
}

