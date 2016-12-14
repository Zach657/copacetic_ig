using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
 // Special button: Deletes previously entered text
public class DeleteButton : ButtonController
{
    // When delete is clicked, delete last entered text and replace with "_". Returns current index
    public override int Clicked(List<string> numbersEntered, int currentIndex)
    {
        if (currentIndex > 0)
        {
            numbersEntered[currentIndex - 1] = "_ ";
            return currentIndex - 1;
        } else
        {
            return currentIndex;
        }
        
    }
}
