using UnityEngine;
/**
 * @author Nathan Pool
 * 
 * Copyright (c) Nathan Pool 2016
 */
public class AttackState : MonoBehaviour, State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;

	// Sets crawler and animator based on state
	// by Nathan Pool
	public AttackState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = moveCrawler.gameObject;
		animator = crawler.GetComponent<Animator> ();
	}
		
	// Performs action with respect to state crawler is in
	// by Nathan Pool
	public void PerformAction() {
		crawler.transform.position += crawler.transform.forward * 1.5f * Time.deltaTime;
		animator.Play ("attack");
		moveCrawler.SetState (moveCrawler.GetCrawlState());
	}

}
