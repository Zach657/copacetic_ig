// <<<<<<< Updated upstream
﻿using UnityEngine;
//using System.Collections;
//
//public class AttackState : State {
//	MoveCrawler moveCrawler;
//	Animator animator;
//	GameObject crawler;
//
//	public AttackState (MoveCrawler moveCrawler) {
//		this.moveCrawler = moveCrawler;
//		crawler = GameObject.FindGameObjectWithTag ("Crawler");
//		animator = crawler.GetComponent<Animator> ();
//	}
//
//	public void PlayerSeen(bool isSeen) {
//		
//	}
//
//	public void PlayerClose() {
////		animator.Play ("attack");
////		moveCrawler.SetState (moveCrawler.GetCrawlState ());
//	}
//
//	public void PerformAction() {
//		crawler.transform.position += crawler.transform.forward * 0.5f * Time.deltaTime;
//		animator.Play ("attack");
//		moveCrawler.SetState (moveCrawler.GetCrawlState());
//	}
//
//}
//||||||| merged common ancestors
//=======
﻿using UnityEngine;
using System.Collections;

public class AttackState : MonoBehaviour, State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;

	public AttackState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
//		crawler = GameObject.FindGameObjectWithTag ("Crawler");
		crawler = moveCrawler.gameObject;
		animator = crawler.GetComponent<Animator> ();
	}

	public void PlayerSeen(bool isSeen) {

	}

	public void PlayerClose() {
//		animator.Play ("attack");
//		moveCrawler.SetState (moveCrawler.GetCrawlState ());
	}

	public void PerformAction() {
		crawler.transform.position += crawler.transform.forward * 1.5f * Time.deltaTime;
		animator.Play ("attack");
		moveCrawler.SetState (moveCrawler.GetCrawlState());
	}

}
//>>>>>>> Stashed changes
