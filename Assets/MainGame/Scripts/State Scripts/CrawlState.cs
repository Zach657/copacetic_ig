using UnityEngine;
using System.Collections;

/**
 * @author Nathan Pool
 * Copyright (c) Nathan Pool 2016
 */
public class CrawlState : MonoBehaviour, State {
    private MoveCrawler moveCrawler;
    private Animator animator;
    private GameObject crawler;
    private GameObject theCrawler;
    private GameObject player;

    private int rotate = 12;
    private int speed = 3;
    private int distance = 1;

    // by Nathan Pool
    public CrawlState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = moveCrawler.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = crawler.GetComponent<Animator> ();

	}

	// Performs action with respect to state crawler is in
	// by Nathan Pool
	public void PerformAction() {
		crawler.transform.Rotate (rotate * Time.deltaTime, 0, 0);
		crawler.transform.position += crawler.transform.forward * speed * Time.deltaTime;
		animator.Play ("crawl");
	}

}