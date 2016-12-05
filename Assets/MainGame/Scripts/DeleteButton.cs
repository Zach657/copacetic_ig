using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DeleteButton : ButtonController
{
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
