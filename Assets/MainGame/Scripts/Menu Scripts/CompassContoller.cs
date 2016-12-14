using UnityEngine;
using System.Collections;
/** 
 * Copyright (C) 2016 - Peter Wages
 **/
public class CompassContoller : MonoBehaviour {
    // Image direction point does not start at North
    private float imageZRotationImageAdjustment = 28.5f;
    // Minimap is upside down, this is the "North" is really "South" adjustment
    private float imageZRotationDirectionAdjustment = 180f;
    private float totalAdjustment;

    // Calculates and sets the total adjustment needed for compass to be accurate
    void Start()
    {
        // The float value total adjustment to the images rotation to account for minimap being upside down and the actual image doesn't point directly north by default
        totalAdjustment = Quaternion.Euler(0, 0, imageZRotationImageAdjustment + imageZRotationDirectionAdjustment).eulerAngles.z;
    }

    // Rotates the compass according to the direction the player is facing
    void Update () {
        Quaternion rotation = Utilities.compass.transform.rotation;
        // Sets the compass' rotation by the player's rotation with the adjustments needed.
        rotation = Quaternion.Euler(0, 0, (Utilities.playerCharacter.transform.rotation.eulerAngles.y + totalAdjustment));
        Utilities.compass.transform.rotation = rotation;


    }
}
