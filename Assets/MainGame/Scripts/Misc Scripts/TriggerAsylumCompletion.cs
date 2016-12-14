using UnityEngine;
using System.Collections;
/** 
 * Copyright (C) 2016 - Peter Wages
 **/
public class TriggerAsylumCompletion : MonoBehaviour {

    // When player walks through locked door, send to next level
    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Player")
        {
            Utilities.sceneController.LoadGameScene(Utilities.LEVELTWO);
        }
    }
}
