using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class DarknessController : MonoBehaviour {
    public bool playerIsVisible = false;
	
	// Freezes Darkness if player can see enemy
	void Update () {
	if (playerIsVisible) ActivateTracking(false);
    else ActivateTracking(true);
	}

    // Toggles the script responsible for tracking the player
    private void ActivateTracking(bool activate)
    {
        this.GetComponent<AICharacterControl>().enabled = activate;
    }
}
