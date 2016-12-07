using UnityEngine;
//using UnityEngine.Microphone;
using System.Collections;

public class PlayerMimic : MonoBehaviour {

	//Most of this code is found on https://support.unity3d.com/hc/en-us/articles/206485253-How-do-I-get-Unity-to-playback-a-Microphone-input-in-real-time-
	void Start () {
		AudioSource audioSource = this.GetComponent<AudioSource> ();
		audioSource.clip = Microphone.Start ("Built-in Microphone", true, 10, 44100);
		audioSource.loop = true;
		while(!(Microphone.GetPosition(null)>500)){}
		audioSource.Play ();
	}
}
