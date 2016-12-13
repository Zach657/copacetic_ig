using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class PlayerController : MonoBehaviour {
    // Future implementation
    private bool readyForDeath = true;

    // On hitting an enemy, player dies
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && readyForDeath)
        {
            SceneController.TriggerDeath();
        }
    }
}
