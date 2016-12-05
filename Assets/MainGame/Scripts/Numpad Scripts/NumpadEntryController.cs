using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class NumpadEntryController : SceneController {

    [SerializeField]
    private Text numpadEntryTextField;
    private List<string> numbers = new List<string>();
    private List<string> resetList;

    private int numberOfNumbersToEnter = 8;
    private int currentIndex = 0;

    private string currentAnswer;
    private const string ANSWER = "0 2 1 5 1 9 9 6";

    // Use this for initialization
    void Start() {
        IntizializeAnswerString();
	}

    private void printString(List<string> list)
    {
        string printable = "";
        foreach(string character in list)
        {
            printable += character;
        }
        currentAnswer = printable;
        numpadEntryTextField.text = printable;
    }

    private void IntizializeAnswerString()
    {
        numbers.Clear();
        for (int i = 0; i < numberOfNumbersToEnter; i++)
        {
            numbers.Add("_ ");
        }
        currentIndex = 0;
        printString(numbers);
    }

    public void ButtonClicked(ButtonController button)
    {
        currentIndex = button.Clicked(numbers, currentIndex);
        printString(numbers);
        if (currentIndex >= numberOfNumbersToEnter)
        {
            ButtonSubmit();
        }
    }

    public void ButtonSubmit()
    {
        if (currentAnswer.Contains(ANSWER))
        {
            Debug.Log("CORRECT");
        }
        else {
            Debug.Log("INCORRECT");
            IntizializeAnswerString();
        }
    }
}
