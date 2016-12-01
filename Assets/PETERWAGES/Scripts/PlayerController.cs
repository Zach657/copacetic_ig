using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class PlayerController : Controller {
    private SceneController sceneController;
    private bool timeToDie = false;

	// Use this for initialization
	void Start () {
        sceneController =  GameObject.Find("Scene Controller").GetComponent<SceneController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // On hitting an enemy, player dies
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && !timeToDie)
        {
            sceneController.TriggerWin();
        }
    }
}
