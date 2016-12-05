using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class NotebookView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<RawImage> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<RawImage> ().enabled && (Input.GetKeyDown (KeyCode.Escape) || Input.GetMouseButtonDown(0))) {
			hideNotebook ();
		}
	}

	void hideNotebook(){
		this.GetComponent<RawImage> ().enabled = false;
	}

	void showNotebook(){
		this.GetComponent<RawImage> ().enabled = true;
	}
}
