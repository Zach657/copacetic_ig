using UnityEngine;
using System.Collections;

public class PounceState : State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;

	public PounceState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = GameObject.FindGameObjectWithTag ("Crawler");
		animator = crawler.GetComponent<Animator> ();
	}

	public void PlayerSeen(bool isSeen) {
		
	}

	public void PlayerClose() {
//		animator.Play ("attack");
//		moveCrawler.SetState (moveCrawler.GetIdleState ());
	}

	public void PerformAction() {
		animator.Play ("pounce");
	}
}
