using UnityEngine;
using System.Collections;
/**
 * @author Nathan Pool
 * Copyright (c) Nathan Pool 2016
 */
public class PounceState : State {
	MoveCrawler moveCrawler;
	Animator animator;
	GameObject crawler;

	// by Nathan Pool
	public PounceState (MoveCrawler moveCrawler) {
		this.moveCrawler = moveCrawler;
		crawler = GameObject.FindGameObjectWithTag ("Crawler");
		animator = crawler.GetComponent<Animator> ();
	}

	// by Nathan Pool
	// the crawler pounces
	public void PerformAction() {
		animator.Play ("pounce");
	}
}
