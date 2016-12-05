//// <<<<<<< Updated upstream
//﻿using UnityEngine;
//using System.Collections;
//
//public class CrawlFastState : State {
//	MoveCrawler moveCrawler;
//	Animator animator;
//	GameObject crawler;
//	GameObject theCrawler;
//	GameObject player;
//	Ray crawlerSight;
//
//	// Only crawls fast when chasing player
//	public CrawlFastState(MoveCrawler moveCrawler) {
//		this.moveCrawler = moveCrawler;
//		crawler = GameObject.FindGameObjectWithTag ("Crawler");
//		theCrawler = GameObject.FindGameObjectWithTag ("Avatar");
//		player = GameObject.FindGameObjectWithTag ("Player");
//		animator = crawler.GetComponent<Animator> ();
//
//	}
//
//	public void PlayerSeen(bool isSeen) {
//		//https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
//		if (Vector3.Distance(crawler.transform.position, player.transform.position) < 1) {
//			moveCrawler.SetState(moveCrawler.GetAttackState());
//		}
//	}
//
//	public void PlayerClose() {
////		animator.Play ("attack");
////		moveCrawler.SetState (moveCrawler.GetCrawlState ());
//	}
//
//	public void PerformAction() {
//		
//		Vector3 targetPosition = player.transform.position;
//		targetPosition.y = crawler.transform.position.y;
//		crawler.transform.LookAt(targetPosition);
//		Vector3 targetAngle = crawler.transform.localEulerAngles;
//		targetAngle.z += 90;
//		crawler.transform.localEulerAngles = targetAngle;
//		crawler.transform.position += crawler.transform.forward * 3 * Time.deltaTime;
//		animator.Play ("crawl_fast");
//	}
//}
//||||||| merged common ancestors
//=======
﻿using UnityEngine;
using System.Collections;

public class CrawlFastState : State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject player;
	Ray crawlerSight;

	// Only crawls fast when chasing player
	public CrawlFastState(MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
//		crawler = GameObject.FindGameObjectWithTag ("Crawler");
		crawler = moveCrawler.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();

	}

	public void PlayerSeen(bool isSeen) {
		//https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
		if (Vector3.Distance(crawler.transform.position, player.transform.position) < 1) {
			moveCrawler.SetState(moveCrawler.GetAttackState());
		}
	}

	public void PlayerClose() {
		//		animator.Play ("attack");
		//		moveCrawler.SetState (moveCrawler.GetCrawlState ());
	}

	public void PerformAction() {
		Vector3 targetPosition = player.transform.position;
		targetPosition.y = crawler.transform.position.y;
		crawler.transform.LookAt(targetPosition);
		Vector3 targetAngle = crawler.transform.localEulerAngles;
		targetAngle.z += 90f;
		crawler.transform.localEulerAngles = targetAngle;
		crawler.transform.position += crawler.transform.forward * 3 * Time.deltaTime;
		animator.Play ("crawl_fast");
	}
}
//>>>>>>> Stashed changes
