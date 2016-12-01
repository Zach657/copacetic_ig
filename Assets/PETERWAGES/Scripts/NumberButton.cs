using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NumberButton : ButtonController {

    [SerializeField]
    private int buttonNumber;

    public override int Clicked(List<string> numbersEntered, int currentIndex)
    {
        numbersEntered[currentIndex] = buttonNumber + " ";
        return currentIndex + 1;
    }
}
