using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ButtonController : MonoBehaviour {

    public abstract int Clicked(List<string> numbersEntered, int currentIndex);
}
