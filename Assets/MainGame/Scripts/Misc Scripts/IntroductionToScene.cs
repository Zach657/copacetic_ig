using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
public class IntroductionToScene : MonoBehaviour
{

    [SerializeField]
    private string[] introductoryMessage;
    private int index = 0;

    // At start of the scene, run the introductory messages
    void Start()
    {
        index = 0;
        StartCoroutine(DelayedStart());

    }

    // Prevents the text object from still being "destroyed" on reload. Waits till game reinstatiates the object
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(RunIntroductoryMessages());
    }

    // Goes through and displays each introduction notification message
    IEnumerator RunIntroductoryMessages()
    {
        if (index < introductoryMessage.Length)
        {
            Utilities.sceneController.DisplayNotification(introductoryMessage[index]);
            yield return new WaitForSeconds(Utilities.NOTIFICATIONDURATION);
            index++;
            StartCoroutine(RunIntroductoryMessages());
        }
        
    }

}