using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFacing : MonoBehaviour
{
    private void LateUpdate()
    {
        // Make the canvas face the main camera
        transform.forward = Camera.main.transform.forward;
    }
}
