using UnityEngine;

public class IdleState : MonoBehaviour, State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject player;

	public IdleState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
//		crawler = GameObject.FindGameObjectWithTag ("Crawler");
		crawler = moveCrawler.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();
	}

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

	public void PerformAction() {
		// crawler just breathes
		animator.Play("idle");
	}

}
//>>>>>>> Stashed changes
