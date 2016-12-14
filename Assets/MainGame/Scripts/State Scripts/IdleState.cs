using UnityEngine;

/**
 * @author Nathan Pool
 * Copyright (c) Nathan Pool 2016
 */
public class IdleState : MonoBehaviour, State {
    private MoveCrawler moveCrawler;
    private Animator animator;
    private GameObject crawler;
    private GameObject player;
    private int distance = 1;

	// by Nathan Pool
	public IdleState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = moveCrawler.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();
	}

	// if the player is seen, the crawler crawls fast
	// by Nathan Pool
	public void PlayerSeen(bool isSeen) {
		animator.Stop ();
		moveCrawler.SetState (moveCrawler.GetCrawlFastState ());

		//https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
		if (Vector3.Distance(crawler.transform.position, player.transform.position) < distance) {
			moveCrawler.SetState(moveCrawler.GetAttackState());
		}
	}

	// the crawler does nothing
	// by Nathan Pool
	public void PerformAction() {
		// crawler just breathes
		animator.Play("idle");
	}

}