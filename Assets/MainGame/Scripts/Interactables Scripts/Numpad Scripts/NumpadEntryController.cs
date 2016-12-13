using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class NumpadEntryController : MonoBehaviour {
    [SerializeField]
    private AudioClip incorrectAnser;
    private List<string> numbers = new List<string>();
    private List<string> resetList;

    public static Puzzle currentPuzzle;
    private int currentIndex = 0;
    private string currentAnswer;

    public GameObject thisUnlockable;

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

    public void IntizializeAnswerString()
    {
        numbers.Clear();
        for (int i = 0; i < currentPuzzle.numberOfNumbersToEnter; i++)
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
        if (currentIndex >= currentPuzzle.numberOfNumbersToEnter)
        {
            ButtonSubmit();
        }
    }

    public void ButtonSubmit()
    {
        if (currentAnswer.Contains(currentPuzzle.answer))
        {
            Utilities.sceneController.ResumeGame();
            thisUnlockable.GetComponent<UnlockableObject>().Unlock();
        }
        else {
            IntizializeAnswerString();
            SceneController.PlaySoundAtPoint(incorrectAnser, Utilities.playerCharacter.transform.position);
        }
    }
}
