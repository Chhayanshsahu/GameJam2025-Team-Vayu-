using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private float _pickupRadius = 2f; // Radius to detect items
    [SerializeField] private LayerMask _pickupLayerMask; // Layer mask for items that can be picked up
    [SerializeField] private Transform _itemHoldPosition; // Position where the item will be held

    private GameObject _heldItem; // Reference to the currently held item
    private bool _isHoldingKey = false; // Track if the player is holding the key

    [Header("Door Settings")]
    [SerializeField] private GameObject door; // Reference to the door GameObject
    [SerializeField] private float openAngle = 90f; // Angle to rotate the door when opening
    [SerializeField] private float openSpeed = 2f; // Speed of the door opening

    private bool _isDoorOpen = false; // Track if the door is open
    private Quaternion _initialDoorRotation; // Initial rotation of the door

    private void Start()
    {
        // Store the initial rotation of the door
        if (door != null)
        {
            _initialDoorRotation = door.transform.rotation;
        }
    }

    private void Update()
    {
        // Pick up an item when "P" is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryPickupItem();
        }

        // Drop the held item when "O" is pressed
        if (Input.GetKeyDown(KeyCode.O))
        {
            DropItem();
        }

        // Open the door when "E" is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }

    void TryPickupItem()
    {
        // Check if the player is already holding an item
        if (_heldItem != null)
        {
            Debug.Log("Already holding " + _heldItem.name);
            return;
        }

        // Detect items within the pickup radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _pickupRadius, _pickupLayerMask);
        if (hitColliders.Length == 0)
        {
            Debug.Log("No item to pickup.");
            return;
        }

        // Find the closest item
        GameObject closestItem = null;
        float closestDistance = float.MaxValue;

        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = hitCollider.gameObject;
            }
        }

        // Pick up the closest item
        if (closestItem != null)
        {
            Pickup(closestItem);
        }
    }

    void Pickup(GameObject item)
    {
        _heldItem = item;

        // Check if the picked-up item is the key
        if (_heldItem.CompareTag("Key"))
        {
            _isHoldingKey = true;
        }

        // Disable physics and collider on the item
        if (item.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
        item.GetComponent<Collider>().enabled = false;

        // Attach the item to the player's hold position
        item.transform.SetParent(_itemHoldPosition);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        Debug.Log("Picked up: " + item.name);
    }

    void DropItem()
    {
        if (_heldItem == null)
        {
            Debug.Log("No item to drop");
            return;
        }

        // Re-enable physics and collider on the item
        if (_heldItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        _heldItem.GetComponent<Collider>().enabled = true;

        // Detach the item from the player
        _heldItem.transform.SetParent(null);

        // Drop the item slightly in front of the player
        _heldItem.transform.position = transform.position + transform.forward * 1.5f;

        // Clear the reference to the held item
        _heldItem = null;
        _isHoldingKey = false;

        Debug.Log("Dropped item.");
    }

    void TryOpenDoor()
    {
        if (_isDoorOpen)
        {
            Debug.Log("The door is already open.");
            return;
        }

        if (_isHoldingKey)
        {
            OpenDoor();
            DestroyKey();
        }
        else
        {
            Debug.Log("Find a key to open the door.");
        }
    }

    void OpenDoor()
    {
        _isDoorOpen = true;

        // Rotate the door to the open position
        if (door != null)
        {
            Quaternion targetRotation = _initialDoorRotation * Quaternion.Euler(0, openAngle, 0);
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetRotation, openSpeed * Time.deltaTime);
        }

        Debug.Log("Door opened.");
    }

    void DestroyKey()
    {
        if (_heldItem != null && _heldItem.CompareTag("Key"))
        {
            Destroy(_heldItem);
            _heldItem = null;
            _isHoldingKey = false;
            Debug.Log("Key destroyed.");
        }
    }

    // Draw the pickup radius in the editor for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _pickupRadius);
    }
}