using UnityEngine;
using System.Collections;

public class MinimapController : MonoBehaviour {

    // Update is called once per frame
    private void Update()
    {
        // check for attempts to open / close minimap on m button press
        SceneController.OpenCloseMenuOnButtonPress(KeyCode.M, Utilities.minimap);
    }
}
