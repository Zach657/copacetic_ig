using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Nathan Pool & Peter Wages
 */
public class TriggerHospitalCompletion : MonoBehaviour {
    // Nathan Pool
    // Dramatic sound effect
    [SerializeField] private AudioClip winClip;
    // Nathan Pool
    // Peter Wages
    [SerializeField]
    private GameObject yourBody;
    [SerializeField]
    private string[] finalSequenceMessages;
    private int index = 0;
    private float audioLength = 7f;
    private float audioDelay = 0;
    private bool winSequenceStarted = false;
    private float waitForNotificationFade = 1f;
    // Peter Wages

        // Resets the index and win sequence started, for reloading purposes. Calculates audio delay for winning sound
    void Start()
    {
        // Peter Wages
        index = 0;
        winSequenceStarted = false;
        // Makes sure the audio delay is accurate
        if (Utilities.NOTIFICATIONDURATION > audioLength)
        {
            audioDelay = Utilities.NOTIFICATIONDURATION - (audioLength - waitForNotificationFade); 
        }
        // Peter Wages
    }

    // Nathan Pool
    // Checks if the player is in range of the ending wake up body object
    void Update () {
		if (SceneController.playerIsNear(yourBody))
        {
            if (!winSequenceStarted)
            {
                StartCoroutine(WinSequence());
                winSequenceStarted = true;
            }
        }
	}
    // Nathan Pool

    // Peter Wages
    // Runs final messages and plays waking up clip, then triggers win screen
    IEnumerator WinSequence() {

        if (index < finalSequenceMessages.Length - 1)
        {
            Utilities.sceneController.DisplayNotification(finalSequenceMessages[index]);
            yield return new WaitForSeconds(Utilities.NOTIFICATIONDURATION);
            index++;
            StartCoroutine(WinSequence());
        } else
        {
            Utilities.sceneController.DisplayNotification(finalSequenceMessages[index]);
            yield return new WaitForSeconds(audioDelay);
            // Nathan Pool
            AudioSource.PlayClipAtPoint(winClip, Utilities.playerCharacter.transform.position);
            // Nathan Pool
            yield return new WaitForSeconds(Utilities.NOTIFICATIONDURATION - (audioDelay - waitForNotificationFade));
            SceneController.TriggerWin();
        }
        // Peter Wages
    }
}
