using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (!GameController.youWin) // Corrected class name to match the previous script
        {
            transform.Rotate(0f, 0f, 90f); // Rotates the image by 90 degrees on the Z-axis
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Update code if needed
    }
}
