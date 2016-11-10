using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter Wages & Zach Greenwell
 **/

public class PlayerSeesEnemy : MonoBehaviour {
    [SerializeField] private Transform darknessTransform;
    [SerializeField] private GameObject darkness;

    void Update()
    {
        SearchForDarkness();
    }

    void SearchForDarkness()
    {
        if (IsInLOS()) { darkness.GetComponent<DarknessController>().playerIsVisible = true; print("DISCOVERED"); }
        else darkness.GetComponent<DarknessController>().playerIsVisible = false;
    }

    /* checks if the player is in the Field of view
         * much of the code in IsInLOS and IsInFOV was obtained from http://unity.grogansoft.com/enemies-that-can-see/
        */
    private bool IsInLOS()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, darknessTransform.position);
        RaycastHit[] rayHits = Physics.RaycastAll(transform.position, darknessTransform.position - transform.position, distanceToPlayer);
       //Used for testing: Debug.DrawRay(transform.position, playerT.position - transform.position, Color.blue);
        foreach (RaycastHit hit in rayHits)
        {
            if (hit.transform.tag != "Darkness")
            {
                return false;
            }
        }
        //player is reachable if no tags were hit
        return IsInFOV();
    }

    private bool IsInFOV()
    {
        Vector3 directionToPlayer = darknessTransform.position - transform.position;  //Direction to player
        Debug.DrawLine(transform.position, darknessTransform.position, Color.magenta); //draws a line to the player from the enemy

        Vector3 lineOfSight = this.transform.forward; // the center of the enemy's field of view
        Debug.DrawLine(transform.position, this.transform.forward, Color.yellow); // draws line to represent center of FOV

        // calculate the angle formed between the player's position and the centre of the enemy's line of sight
        float angle = Vector3.Angle(directionToPlayer, lineOfSight);

        // if the player is visible and within 65 degrees of the central FOV ray
        if (angle < 65)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}