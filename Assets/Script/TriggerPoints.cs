using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TriggerPoints : MonoBehaviour
{
    [SerializeField] private string tagFilter; // Tag to filter objects
    [SerializeField] private UnityEvent OnTriggerEnterEvent; // Events for entering the trigger
    [SerializeField] private UnityEvent OnTriggerExitEvent; // Events for exiting the trigger
    [SerializeField] private bool destroyOnTriggerEnter = false; // Option to destroy the trigger object

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter)) return;

        OnTriggerEnterEvent.Invoke();
        Debug.Log("✅ Player entered the room!");

        if (destroyOnTriggerEnter)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter)) return;

        OnTriggerExitEvent.Invoke();
    }
}
