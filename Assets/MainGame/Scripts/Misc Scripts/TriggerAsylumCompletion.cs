using UnityEngine;
using System.Collections;

public class TriggerAsylumCompletion : Controller {
    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Player")
        {
            LoadGameScene(LEVELTWO);
        }
    }
}
