using UnityEngine;
using System.Collections;
/**
 * @author Nathan Pool
 * Copyright (c) Nathan Pool 2016
 */
public class CrawlFastState : MonoBehaviour, State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject player;
	Ray crawlerSight;

	// Only crawls fast when chasing player
	// Nathan Pool
	public CrawlFastState(MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = moveCrawler.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();

	}

	public void PlayerSeen(bool isSeen) {

	}

	// Performs action with respect to state crawler is in
	// by Nathan Pool
	public void PerformAction() {
		Vector3 targetPosition = player.transform.position;
		targetPosition.y = crawler.transform.position.y;
		crawler.transform.LookAt(targetPosition);
		Vector3 targetAngle = crawler.transform.localEulerAngles;
		targetAngle.z += 90f;
		crawler.transform.localEulerAngles = targetAngle;
		crawler.transform.position += crawler.transform.forward * 7 * Time.deltaTime;
		animator.Play ("crawl_fast");

		//https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
		if (Vector3.Distance(crawler.transform.position, player.transform.position) < 3) {
			moveCrawler.SetState(moveCrawler.GetAttackState());
		}
	}
}