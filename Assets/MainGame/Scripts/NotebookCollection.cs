using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/** 
 * Copyright (C) 2016 - James Greenwell
 **/

public class NotebookCollection : MonoBehaviour {

	public void unlockNotebook(int id){
		Component notebookObject = this.gameObject.GetComponent ("Notebook" + id);
		notebookObject.GetComponent<Button>().enabled = true;
		notebookObject.GetComponent<Text> ().color = new Color (0f, 255f, 0f, 255f);
	}
}
