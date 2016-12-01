//// <<<<<<< Updated upstream
//﻿using UnityEngine;
//using System.Collections;
//
///**
// * @author Nathan Pool
// * 
// */
//
//// https://docs.unity3d.com/ScriptReference/Animator.html
//public class MoveCrawler : MonoBehaviour {
//
//	// Future implementations of activity
//	private float distanceTravelled;
//	private float timeWalked;
//
//	// The object
//	private GameObject crawler;
//
//	// The crawler's animation
//	private Animator animator;
//	private Animation animation;
//	private RuntimeAnimatorController controller;
//	private AnimationClip[] clips;
//
//	// The crawler's movement
//	private float xSpeed;
//	private float ySpeed;
//	private float zSpeed;
//
//	// The crawler's line of vision
//	private Ray crawlerSight;
//	private LineRenderer laser;
//	private RaycastHit aHit;
//	private const int SIGHT_LENGTH = 30;
//
//	// State pattern
//	private bool isSeen;
//	private State currentState;
//	private State idleState;
//	private State crawlState;
//	private State crawlFastState;
//	private State pounceState;
//	private State attackState;
//
//
//	// Use this for initialization
//	void Start () {
//		distanceTravelled = 0;
//		timeWalked += Time.deltaTime;
//		crawler = GameObject.FindGameObjectWithTag ("Crawler");
//		animator = GetComponent<Animator> ();
//		controller = animator.runtimeAnimatorController;
//		clips = controller.animationClips;
//
//		xSpeed = 0.02f;
//		ySpeed = 0.05f;
//		zSpeed = 0.05f;
//
//		// laser is for debugging purposes
//		laser = GetComponent<LineRenderer>();
//		crawlerSight = new Ray (crawler.transform.position, 
//			crawler.transform.TransformDirection(Vector3.forward));
//
//		// State pattern
//		isSeen = false;
//		idleState = new IdleState (this);
//		crawlState = new CrawlState(this);
//		crawlFastState = new CrawlFastState (this);
//		pounceState = new PounceState (this);
//		attackState = new AttackState (this);
//		currentState = crawlState;
//
//	}
//
//	// Update is called once per frame
//	void Update () {
////		crawler.transform.Rotate (20 * Time.deltaTime, 0, 0);
////		crawler.transform.position += crawler.transform.forward * Time.deltaTime;
//
//		// moves origin of ray to position of crawler - changes direction respectively
//		crawlerSight.origin = crawler.transform.position;
//		crawlerSight.direction = crawler.transform.forward;
//
//		// debugging - makes ray visible
////		laser.SetPosition (0, crawlerSight.origin);
////		laser.SetPosition (1, crawlerSight.GetPoint (SIGHT_LENGTH));
////		laser.enabled = true;
//
//		// After State pattern
//		PerformAction();
//		if (Physics.Raycast (crawler.transform.position, crawlerSight.direction, out aHit, SIGHT_LENGTH) &&
//		    aHit.collider.tag.Equals ("Player")) {
//			isSeen = true;
//			PlayerSeen (isSeen);
//		}
//
//		// Before State pattern
////		if (xSpeed > 0) {
////			animator.Play ("crawl");
////		} else if (xSpeed > .1f) {
////			animator.Play ("crawl_fast");
////		} else if (xSpeed == 0 && zSpeed == 0) {
////			animator.Play ("Idle");
////		}
//	}
//
//	public void PerformAction() {
//		currentState.PerformAction ();
//	}
//
//	public void PlayerSeen(bool isSeen) {
//		currentState.PlayerSeen (isSeen);
//	}
//
//	public GameObject GetCrawler() {
//		return crawler;
//	}
//
//	public State GetIdleState() {
//		return idleState;
//	}
//
//	public State GetCrawlState() {
//		return crawlState;
//	}
//
//	public State GetCrawlFastState() {
//		return crawlFastState;
//	}
//
//	public State GetAttackState() {
//		return attackState;
//	}
//
//	public State GetPounceState() {
//		return pounceState;
//	}
//
//	public State GetState() {
//		return currentState;
//	}
//
//	public void SetState(State newState) {
//		currentState = newState;
//	}
//}
//||||||| merged common ancestors
//=======
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

	// The crawler's line of vision
	private Ray crawlerSight;
//	private LineRenderer laser;
	private RaycastHit aHit;
	private const int SIGHT_LENGTH = 30;

	// State pattern
	private bool isSeen;
	private State currentState;
	private State idleState;
	private State crawlState;
	private State crawlFastState;
	private State pounceState;
	private State attackState;

	// Use this for initialization
	void Start () {
		timeWalked += Time.deltaTime;
		crawler = this.gameObject;

		// laser is for debugging purposes
	//	laser = GetComponent<LineRenderer>();

		crawlerSight = new Ray (crawler.transform.position, 
			crawler.transform.TransformDirection(Vector3.forward));

		// State pattern
		isSeen = false;
//		idleState = new IdleState (this);
//		crawlState = new CrawlState(this);
//		crawlFastState = new CrawlFastState (this);
//		pounceState = new PounceState (this);
//		attackState = new AttackState (this);
		currentState = new CrawlState(this);

	}

	// Update is called once per frame
	void Update () {
		//		crawler.transform.Rotate (20 * Time.deltaTime, 0, 0);
		//		crawler.transform.position += crawler.transform.forward * Time.deltaTime;
		Vector3 eyeSight = crawler.transform.position;
		eyeSight.y += 3;

		// moves origin of ray to position of crawler - changes direction respectively
		crawlerSight.origin = eyeSight;
		crawlerSight.direction = crawler.transform.forward;

		// debugging - makes ray visible
//		laser.SetPosition (0, eyeSight);
//		laser.SetPosition (1, crawlerSight.GetPoint (SIGHT_LENGTH));
//		laser.enabled = true;
//

		// After State pattern
		PerformAction();
		if (Physics.Raycast (eyeSight, crawlerSight.direction, out aHit, SIGHT_LENGTH) &&
			aHit.collider.tag.Equals ("Player")) {
			isSeen = true;
			PlayerSeen (isSeen);
		}

		// Before State pattern
		//		if (xSpeed > 0) {
		//			animator.Play ("crawl");
		//		} else if (xSpeed > .1f) {
		//			animator.Play ("crawl_fast");
		//		} else if (xSpeed == 0 && zSpeed == 0) {
		//			animator.Play ("Idle");
		//		}
	}

	public void PerformAction() {
		currentState.PerformAction();
	}

	public void PlayerSeen(bool isSeen) {
		currentState.PlayerSeen (isSeen);
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
//>>>>>>> Stashed changes
