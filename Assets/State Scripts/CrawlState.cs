<<<<<<< Updated upstream
﻿using UnityEngine;
using System.Collections;

public class CrawlState : State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject theCrawler;
	GameObject player;

	public CrawlState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = GameObject.FindGameObjectWithTag ("Crawler");
		theCrawler = GameObject.Find ("Crawler");
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();

	}

	public void PlayerSeen(bool isSeen) {
		moveCrawler.SetState (moveCrawler.GetCrawlFastState());

		//https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
		if (Vector3.Distance(crawler.transform.position, player.transform.position) < 1) {
			moveCrawler.SetState(moveCrawler.GetAttackState());
		}
	}

	public void PlayerClose() {
//		animator.Play ("attack");
	}

	public void PerformAction() {
		crawler.transform.Rotate (20 * Time.deltaTime, 0, 0);
		crawler.transform.position += crawler.transform.forward * Time.deltaTime;
		animator.Play ("crawl");
	}

}
||||||| merged common ancestors
=======
﻿using UnityEngine;
using System.Collections;

public class CrawlState : State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject theCrawler;
	GameObject player;

	public CrawlState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
//		crawler = GameObject.FindGameObjectWithTag ("Crawler");
		crawler = moveCrawler.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();

	}

	public void PlayerSeen(bool isSeen) {
		moveCrawler.SetState (moveCrawler.GetCrawlFastState());

		//https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
		if (Vector3.Distance(crawler.transform.position, player.transform.position) < 1) {
			moveCrawler.SetState(moveCrawler.GetAttackState());
		}
	}

	public void PlayerClose() {
		//		animator.Play ("attack");
	}

	public void PerformAction() {
		crawler.transform.Rotate (20 * Time.deltaTime, 0, 0);
		crawler.transform.position += crawler.transform.forward * Time.deltaTime;
		animator.Play ("crawl");
	}

}
>>>>>>> Stashed changes
