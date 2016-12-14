using UnityEngine;
//using UnityEngine.Microphone;
using System.Collections;
/** 
 * Copyright (C) 2016 - James Greenwell
 **/

    //Mimics the player's voice and plays it back to them
public class PlayerMimic : MonoBehaviour {
	private AudioSource audioSource;
	private const int MICDELAY = 500;
	//Most of this code is found on https://support.unity3d.com/hc/en-us/articles/206485253-How-do-I-get-Unity-to-playback-a-Microphone-input-in-real-time-
	void Start () {
		audioSource = this.GetComponent<AudioSource> ();
		audioSource.clip = Microphone.Start (null, true, 10, 44100);
		audioSource.loop = true;
		while(!(Microphone.GetPosition(null)>MICDELAY)){}
		audioSource.Play ();
	}
	void Update(){
		audioSource.pitch = 1;
	}
}
