using UnityEngine;

/**
 * @author Nathan Pool
 * Copyright (c) Nathan Pool 2016
 */
public class IdleState : MonoBehaviour, State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject player;

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
		if (Vector3.Distance(crawler.transform.position, player.transform.position) < 1) {
			moveCrawler.SetState(moveCrawler.GetAttackState());
		}
	}

	public void PlayerClose() {
		//		animator.Play ("attack");
	}

	// the crawler does nothing
	// by Nathan Pool
	public void PerformAction() {
		// crawler just breathes
		animator.Play("idle");
	}

}