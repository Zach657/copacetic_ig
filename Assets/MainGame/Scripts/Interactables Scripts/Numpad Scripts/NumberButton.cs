using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
public class NumberButton : ButtonController {

    [SerializeField]
    private int buttonNumber;

    // When button is clicked, add the number to the entered list and returns the updated index
    public override int Clicked(List<string> numbersEntered, int currentIndex)
    {
        numbersEntered[currentIndex] = buttonNumber + " ";
        return currentIndex + 1;
    }
}
