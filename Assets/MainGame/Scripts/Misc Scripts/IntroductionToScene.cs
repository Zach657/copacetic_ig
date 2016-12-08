using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
public class IntroductionToScene : SceneController
{

    [SerializeField]
    private string[] introductoryMessage;
    private int index = 0;

    // At start of the scene, run the introductory messages
    void Start()
    {
        index = 0;
        StartCoroutine(RunIntroductoryMessages());

    }

    // Goes through and displays each introduction notification message
    IEnumerator RunIntroductoryMessages()
    {
        if (index < introductoryMessage.Length)
        {
            DisplayNotification(introductoryMessage[index]);
            yield return new WaitForSeconds(10);
            index++;
            StartCoroutine(RunIntroductoryMessages());
        }
        
    }

}