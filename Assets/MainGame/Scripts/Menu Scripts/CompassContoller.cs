using UnityEngine;
using System.Collections;

public class CompassContoller : MonoBehaviour {
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform compassImage;

    [SerializeField]
    // Image direction point does not start at North
    private float imageZRotationImageAdjustment = 28.5f;
    // Minimap is upside down, this is the "North" is really "South" adjustment
    private float imageZRotationDirectionAdjustment = 180f;
    private float totalAdjustment;

    void Start()
    {
        // The float value total adjustment to the images rotation to account for minimap being upside down and the actual image doesn't point directly north by default
        totalAdjustment = Quaternion.Euler(0, 0, imageZRotationImageAdjustment + imageZRotationDirectionAdjustment).eulerAngles.z;
    }

    // Update is called once per frame
    void Update () {
        Quaternion rotation = compassImage.rotation;
        // Sets the compass' rotation by the player's rotation with the adjustments needed.
        rotation = Quaternion.Euler(0, 0, (playerTransform.rotation.eulerAngles.y + totalAdjustment));
        compassImage.rotation = rotation;


    }
}
