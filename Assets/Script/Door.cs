using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float closeAngle = 0f;
    [SerializeField] private float openSpeed = 2f;
   // [SerializeField] private float interactionDistance = 3f;

    private bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);
            Debug.Log("Door is opening...");
        }
        else 
        {
            isOpen = false;
            targetRotation = initialRotation * Quaternion.Euler(0, closeAngle, 0);
        }
    }
}
