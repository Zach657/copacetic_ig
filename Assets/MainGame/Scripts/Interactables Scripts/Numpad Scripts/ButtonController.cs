using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/
// Button inheritance hierarchy
public abstract class ButtonController : MonoBehaviour {

    // When the button is clicked, do ...
    public abstract int Clicked(List<string> numbersEntered, int currentIndex);
}
