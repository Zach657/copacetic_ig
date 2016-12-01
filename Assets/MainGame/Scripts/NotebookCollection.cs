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
        Text notebookText = notebookObject.GetComponent<Text>();
        string text = "*" + notebookText.text;
        notebookText.text = text;

    }
}
