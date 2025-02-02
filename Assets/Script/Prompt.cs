using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    [Header("UI Settings")]
    public Text promptText; // Reference to the UI Text element

    [Header("Movement Prompt Settings")]
    public float minDistanceToTrigger = 10f; // Minimum distance the player must move
    public string movementPrompt = "Find the keys to escape. Face what you’ve buried.";

    [Header("Room Prompt Settings")]
    public string roomPrompt = "A memory lost… a moment once golden. Can you piece it back together?";
    public string roomTag = "Room1"; // Tag for the room trigger

    private Vector3 playerStartPosition;
    private GameObject player;
    private bool hasMovementPromptBeenShown = false;
    private bool hasRoomPromptBeenShown = false;

    void Start()
    {
        // Find and cache the player GameObject
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("🚨 ERROR: Player not found! Make sure the player GameObject has the 'Player' tag.");
            return;
        }

        // Get the player's starting position
        playerStartPosition = player.transform.position;

        // Clear the prompt text at the start
        if (promptText != null)
        {
            promptText.text = "";
        }
        else
        {
            Debug.LogError("🚨 ERROR: No UI Text assigned for the prompt!");
        }

        // Start checking player movement
        StartCoroutine(CheckPlayerMovement());
    }

    IEnumerator CheckPlayerMovement()
    {
        while (true)
        {
            // Check if the player has moved enough to trigger the movement prompt
            if (!hasMovementPromptBeenShown)
            {
                float distanceMoved = Vector3.Distance(playerStartPosition, player.transform.position);
                if (distanceMoved >= minDistanceToTrigger)
                {
                    ShowPrompt(movementPrompt);
                    hasMovementPromptBeenShown = true; // Ensure the prompt only shows once
                }
            }

            yield return null; // Wait until the next frame
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("➡ Trigger Entered by: " + other.gameObject.name); // Debug log

        // Ensure the player enters the room trigger
        if (!hasRoomPromptBeenShown && other.CompareTag("Player"))
        {
            Debug.Log("✅ Player entered the room!");

            ShowPrompt(roomPrompt);
            hasRoomPromptBeenShown = true; // Ensure it only triggers once
        }
    }

    void ShowPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.text = message;
            Debug.Log("📝 Showing prompt: " + message); // Debug log
        }

        // Hide the prompt after a delay
        StartCoroutine(HidePromptAfterDelay(5f));
    }

    IEnumerator HidePromptAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (promptText != null)
        {
            promptText.text = ""; // Clear the prompt
        }
    }
}
