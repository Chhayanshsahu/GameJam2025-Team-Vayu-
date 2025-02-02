using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerPoints : MonoBehaviour
{
    [Header("Trigger Settings")]
    [Tooltip("Tag to filter objects that can trigger this event.")]
    [SerializeField] private string tagFilter;

    [Header("Events")]
    [Tooltip("Event invoked when an object enters the trigger.")]
    [SerializeField] private UnityEvent OnTriggerEnterEvent;

    [Tooltip("Event invoked when an object exits the trigger.")]
    [SerializeField] private UnityEvent OnTriggerExitEvent;

    [Header("Options")]
    [Tooltip("Destroy this object when the player interacts with it.")]
    [SerializeField] private bool destroyOnInteract = true;

    [Header("UI")]
    [Tooltip("UI Text element to display interaction prompts.")]
    [SerializeField] private Text interactionPrompt;

    private bool isPlayerInTrigger = false; // Track if the player is in the trigger zone

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the correct tag (if a tag filter is set)
        if (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter)) return;

        isPlayerInTrigger = true;
        OnTriggerEnterEvent.Invoke();
        

        // Show interaction prompt
        if (interactionPrompt != null)
        {
            interactionPrompt.text = "Press E to interact";
            interactionPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object has the correct tag (if a tag filter is set)
        if (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter)) return;

        isPlayerInTrigger = false;
        OnTriggerExitEvent.Invoke();

        // Hide interaction prompt
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the player is in the trigger zone and presses the "E" key
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Debug.Log("Player interacted with the trigger!");

        // Invoke any additional logic here (e.g., load a scene, solve a puzzle, etc.)
        SolvePuzzle();

        // Destroy the trigger GameObject if enabled
        if (destroyOnInteract)
        {
            Destroy(gameObject);
        }
    }

    private void SolvePuzzle()
    {
        SceneManager.LoadScene("JigSawPuzzle");
    
         // Replace "PuzzleScene" with your scene name
    }
}