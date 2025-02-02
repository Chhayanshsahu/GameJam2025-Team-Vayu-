using UnityEngine;
using UnityEngine.UI; // Add this to access UI elements
using System.Collections; // Needed for IEnumerator

public class PlayerDistanceTrigger : MonoBehaviour
{
    public float distanceToTrigger1 = 5f; // Distance for the first prompt
    public float distanceToTrigger2 = 15f; // Distance for the second prompt
    private Vector3 startingPosition;
    private float distanceTraveled = 0f;

    public Text promptText; // Reference to the UI Text component

    private bool prompt1Triggered = false; // Track if the first prompt has been triggered
    private bool prompt2Triggered = false; // Track if the second prompt has been triggered

    void Start()
    {
        // Save the starting position of the player
        startingPosition = transform.position;

        // Make sure the prompt text is initially empty
        promptText.text = "";
    }

    void Update()
    {
        // Calculate how far the player has moved from the starting position
        distanceTraveled = Vector3.Distance(startingPosition, transform.position);

        // Check for distance and show prompts
        if (!prompt1Triggered && distanceTraveled >= distanceToTrigger1 && distanceTraveled < distanceToTrigger2)
        {
            ShowPrompt1();
        }
        else if (!prompt2Triggered && distanceTraveled >= distanceToTrigger2)
        {
            ShowPrompt2();
        }
    }

    void ShowPrompt1()
    {
        // Show first prompt message on the UI Text
        promptText.text = "Find the keys to escape. Face what you’ve buried.";
        prompt1Triggered = true; // Mark the first prompt as triggered

        // Start coroutine to clear prompt 1 after 4 seconds
        StartCoroutine(ClearPrompt1AfterDelay());
    }

    void ShowPrompt2()
    {
        // Show second prompt message on the UI Text
        promptText.text = "A memory lost… a moment once golden. Can you piece it back together?";
        prompt2Triggered = true; // Mark the second prompt as triggered

        // Start coroutine to clear prompt 2 after 5 seconds
        StartCoroutine(ClearPrompt2AfterDelay());
    }

    // Coroutine to clear Prompt 1 after 4 seconds
    IEnumerator ClearPrompt1AfterDelay()
    {
        yield return new WaitForSeconds(4f); // Wait for 4 seconds
        promptText.text = ""; // Clear the text
    }

    // Coroutine to clear Prompt 2 after 5 seconds
    IEnumerator ClearPrompt2AfterDelay()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        promptText.text = ""; // Clear the text
    }
}
