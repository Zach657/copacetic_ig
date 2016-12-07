using UnityEngine;
using System.Collections;

public class HandleCode : MonoBehaviour 
{
	private string answer;
	private string input;
	private GameObject door;
	private AsylumDoorInteract interaction;

	public void Start() {
		answer = "369369147369";
		PlayerPrefs.SetString ("input", "");
		door = GameObject.Find ("door");
		interaction = door.GetComponent<AsylumDoorInteract> ();
	}

	public void Clicky (){
		GetComponent<AudioSource>().Play();
		string currentInput = PlayerPrefs.GetString ("input");
		PlayerPrefs.SetString ("input", currentInput + this.gameObject.name);
		print (PlayerPrefs.GetString("input"));

	}

	public void OnEnterPressed() {
		string input = PlayerPrefs.GetString ("input");
		if (input.Equals (answer)) {
			
			print ("Open the odor");
			GameObject.Find ("CodeInput").SetActive (false);
			interaction.SetUnlocked ();
			Cursor.visible = false;
		} else {
			print ("WRONG");
			GameObject.Find ("CodeInput").SetActive (false);
			PlayerPrefs.SetString ("input", "");
			Cursor.visible = false;
		}
	}

	public void OnClearPressed() {
		PlayerPrefs.SetString ("input", "");
	}


}
