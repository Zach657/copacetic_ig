﻿using UnityEngine;
using System.Collections;

/**
 * @author Nathan Pool
 * 
 */

// https://docs.unity3d.com/ScriptReference/Animator.html
public class MoveCrawler : MonoBehaviour {

	// Future implementations of activity
	private float distanceTravelled;
	private float timeWalked;

	// The object
	private GameObject crawler;

	// The crawler's animation
	private RuntimeAnimatorController controller;

	//private LineRenderer laser;
	private RaycastHit aHit;
	private const int SIGHT_LENGTH = 70;
    private const float radiusRange = 10f;

    // State pattern
    //private bool isSeen;
    private State currentState;
	private State idleState;
	private State crawlState;
	private State crawlFastState;
	private State pounceState;
	private State attackState;

	// Sets the FOV sensitivity
	private int angleDetect = 65;

	// Use this for initialization
	void Start () {
		timeWalked += Time.deltaTime;
		crawler = this.gameObject;

		// State pattern
		//isSeen = false;
		currentState = new CrawlState(this);

	}

	// Update is called once per frame
	void Update () {
		// After State pattern
		PerformAction();
		if (IsInLOS()){
			currentState = new CrawlFastState (this);
		} else {
            if (currentState.GetType() != typeof(CrawlState))
            {
                currentState = new CrawlState(this);
            }
		}
	}

	//Taken from Peter and Zach's FPS project
	private bool IsInLOS(){
		Vector3 crawlerPosition = new Vector3 (crawler.transform.position.x,crawler.transform.position.y + 3f, crawler.transform.position.z);
		float distanceToPlayer = Vector3.Distance(crawlerPosition, Utilities.playerCharacter.transform.position);
		if (distanceToPlayer < 15) {
			return true;
		} else if(distanceToPlayer < 45)
        {
			RaycastHit[] rayHits = Physics.RaycastAll (crawlerPosition, Utilities.playerCharacter.transform.position - crawlerPosition, distanceToPlayer);
			Debug.DrawRay (crawlerPosition, Utilities.playerCharacter.transform.position - crawlerPosition, Color.blue);
			foreach (RaycastHit hit in rayHits) {            
				if (hit.transform.tag != "Player") {
					return false;
				}
			}
			//player is reachable if no tags were hit
			return IsInFOV (crawlerPosition);
		}
		return false;
	}

	//Taken from Peter and Zach's FPS project
	private bool IsInFOV(Vector3 crawlerPosition){
		Vector3 directionToPlayer = Utilities.playerCharacter.transform.position - crawlerPosition;  //Direction to player
		Debug.DrawLine(crawlerPosition, Utilities.playerCharacter.transform.position, Color.magenta); //draws a line to the player from the enemy

		Vector3 lineOfSight = this.transform.forward; // the center of the enemy's field of view
		Debug.DrawLine(crawlerPosition, crawler.transform.forward, Color.yellow); // draws line to represent center of FOV

		// calculate the angle formed between the player's position and the centre of the enemy's line of sight
		float angle = Vector3.Angle(directionToPlayer, lineOfSight);

		// if the player is visible and within 65 degrees of the central FOV ray
		if (angle < angleDetect) {
			return true;
		}  else {
			return false;
		}
	}

	public void PerformAction() {
		currentState.PerformAction();
	}

	public GameObject GetCrawler() {
		return crawler;
	}

	public State GetIdleState() {
		return new IdleState (this);
	}

	public State GetCrawlState() {
		return new CrawlState (this);
	}

	public State GetCrawlFastState() {
		return new CrawlFastState (this);
	}

	public State GetAttackState() {
		return new AttackState (this);
	}

	public State GetPounceState() {
		return new PounceState (this);
	}

	public State GetState() {
		return currentState;
	}

	public void SetState(State newState) {
		currentState = newState;
	}
}