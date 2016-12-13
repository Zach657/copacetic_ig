using UnityEngine;
using System.Collections;

public class TriggerAsylumCompletion : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Player")
        {
            Utilities.sceneController.LoadGameScene(Utilities.LEVELTWO);
        }
    }
}
