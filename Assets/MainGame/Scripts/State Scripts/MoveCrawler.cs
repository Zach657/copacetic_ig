using UnityEngine;
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
	//private LineRenderer laser;
	private RaycastHit aHit;
	private const int SIGHT_LENGTH = 70;

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
		//laser = GetComponent<LineRenderer>();

		crawlerSight = new Ray (crawler.transform.position, 
			crawler.transform.TransformDirection(Vector3.forward));

		// State pattern
		isSeen = false;
		currentState = new CrawlState(this);

	}

	// Update is called once per frame
	void Update () {
		Vector3 eyeSight = crawler.transform.position;
		eyeSight.y += 3;

		// moves origin of ray to position of crawler - changes direction respectively
		crawlerSight.origin = eyeSight;
		crawlerSight.direction = crawler.transform.forward;

		// debugging - makes ray visible
		//laser.SetPosition (0, eyeSight);
		//laser.SetPosition (1, crawlerSight.GetPoint (SIGHT_LENGTH));
		//laser.enabled = true;


		// After State pattern
		PerformAction();
		if (Physics.Raycast (eyeSight, crawlerSight.direction, out aHit, SIGHT_LENGTH) &&
		    aHit.collider.tag.Equals ("Player")) {
			currentState = new CrawlFastState (this);
		} else {
            if (currentState.GetType() != typeof(CrawlState))
            {
                currentState = new CrawlState(this);
            }
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