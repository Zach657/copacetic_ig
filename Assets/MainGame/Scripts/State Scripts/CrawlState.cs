using UnityEngine;
using System.Collections;

/**
 * @author Nathan Pool
 * Copyright (c) Nathan Pool 2016
 */
public class CrawlState : MonoBehaviour, State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject theCrawler;
	GameObject player;

	// by Nathan Pool
	public CrawlState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = moveCrawler.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();

	}

	// If the player is seen, the state is set to CrawlFast
	// by Nathan Pool
	public void PlayerSeen(bool isSeen) {
		moveCrawler.SetState (moveCrawler.GetCrawlFastState());

		//https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
		if (Vector3.Distance(crawler.transform.position, player.transform.position) < 1) {
			moveCrawler.SetState(moveCrawler.GetAttackState());
		}
	}

	// Performs action with respect to state crawler is in
	// by Nathan Pool
	public void PerformAction() {
		crawler.transform.Rotate (12 * Time.deltaTime, 0, 0);
		crawler.transform.position += crawler.transform.forward * 3 * Time.deltaTime;
		animator.Play ("crawl");
	}

}