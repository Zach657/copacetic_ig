using UnityEngine;
using System.Collections;

/**
 * @author Nathan Pool
 */
public class CrawlState : MonoBehaviour, State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;
	GameObject theCrawler;
	GameObject player;

	public CrawlState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
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
	}

	public void PerformAction() {
		crawler.transform.Rotate (12 * Time.deltaTime, 0, 0);
		crawler.transform.position += crawler.transform.forward * 3 * Time.deltaTime;
		animator.Play ("crawl");
	}

}