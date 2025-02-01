using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
public float openAngle = 90f; // Angle to rotate the door when opening
    public float openSpeed = 2f; // Speed of the door opening
    public float interactionDistance = 3f; // Distance within which the player can interact with the door

    private bool isOpen = false; // Track if the door is open
    private Quaternion initialRotation; // Initial rotation of the door
    private Quaternion targetRotation; // Target rotation when opening/closing

    void Start()
    {
        // Store the initial rotation of the door
        initialRotation = transform.rotation;
        targetRotation = initialRotation;

     
    }

    void Update()
    {
        // Smoothly rotate the door to the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);

   

        // Check if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if the player is close enough to the door
            if (IsPlayerNearby())
            {
                ToggleDoor();
            }
        }
    }

    // Toggle the door's open/close state
    void ToggleDoor()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            // Rotate the door to the open position
            targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);
           
        }
        else
        {
            // Rotate the door back to the initial position
            targetRotation = initialRotation;
            
        }
    }

    // Check if the player is nearby
    bool IsPlayerNearby()
    {
        // Find the player by tag (make sure your player GameObject has the "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Calculate the distance between the player and the door
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance <= interactionDistance;
        }

        return false;
    }
}