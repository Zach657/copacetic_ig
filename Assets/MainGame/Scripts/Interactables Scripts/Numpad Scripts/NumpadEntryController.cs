using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
 // Controls the numpad and its entry 
public class NumpadEntryController : MonoBehaviour {
    [SerializeField]
    private AudioClip incorrectAnser;
    private List<string> numbers = new List<string>();
    private List<string> resetList;

    public static Puzzle currentPuzzle;
    private int currentIndex = 0;
    private string currentAnswer;

    [HideInInspector]
    public GameObject thisUnlockable;

    // Prints the current entered text
    private void printString(List<string> list)
    {
        string printable = "";
        foreach(string character in list)
        {
            printable += character;
        }
        currentAnswer = printable;
        Utilities.numpadEntryTextField.text = printable;
    }

    // Initializes the current puzzle's answer
    public void InitizializeAnswerString()
    {
        numbers.Clear();
        for (int i = 0; i < currentPuzzle.numberOfNumbersToEnter; i++)
        {
            numbers.Add("_ ");
        }
        currentIndex = 0;
        printString(numbers);
    }

    // Insert the button clicked to current entered string when clicked
    public void ButtonClicked(ButtonController button)
    {
        currentIndex = button.Clicked(numbers, currentIndex);
        printString(numbers);
        if (currentIndex >= currentPuzzle.numberOfNumbersToEnter)
        {
            ButtonSubmit();
        }
    }

    // Compares the current entered string to the answer
    public void ButtonSubmit()
    {
        if (currentAnswer.Contains(currentPuzzle.answer))
        {
            Utilities.sceneController.ResumeGame();
            thisUnlockable.GetComponent<UnlockableObject>().Unlock();
        }
        else {
            InitizializeAnswerString();
            SceneController.PlaySoundAtPoint(incorrectAnser, Utilities.playerCharacter.transform.position);
        }
    }
}
