using UnityEngine;
using System.Collections;

public class GameStatusController : MonoBehaviour {

    [SerializeField] private GameObject GameUI;
    
    [SerializeField] private string brainsCollectedKey;
    
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    // Called when the player collects a brain
    public void BrainCollected(){
        int numBrains = PlayerPrefs.GetInt(brainsCollectedKey);
        if(numBrains != null){
            numBrains = numBrains + 1;
        }
        else{
            numBrains = 1;
            
        }
        PlayerPrefs.SetInt(brainsCollectedKey, numBrains);
        recallMemory(numBrains);
    }
    
    //Displays a player memory to the screen upon brain collection
    private void recallMemory(int memNum){
        
    }
    
    // Called when the player collects a notebook
    public void NotebookCollected(){
        
    }
}
