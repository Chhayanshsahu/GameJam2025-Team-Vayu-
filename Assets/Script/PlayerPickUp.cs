using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private float _pickupRadius = 2f;
    [SerializeField] private LayerMask _pickupLayerMask;
    [SerializeField] private Transform _itemHoldPosition;

    private GameObject _heldItem;
    private bool _isHoldingKey = false;

    [Header("Door Settings")]
    [SerializeField] private DoorController door; // Reference to the DoorController script

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryPickupItem();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            DropItem();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }

    void TryPickupItem()
    {
        if (_heldItem != null)
        {
            Debug.Log("Already holding " + _heldItem.name);
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _pickupRadius, _pickupLayerMask);
        if (hitColliders.Length == 0)
        {
            Debug.Log("No item to pick up.");
            return;
        }

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

        if (closestItem != null)
        {
            Pickup(closestItem);
        }
    }

    void Pickup(GameObject item)
    {
        _heldItem = item;

        if (_heldItem.CompareTag("Key"))
        {
            _isHoldingKey = true;
        }

        if (item.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
        item.GetComponent<Collider>().enabled = false;

        item.transform.SetParent(_itemHoldPosition);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        Debug.Log("Picked up: " + item.name);
    }

    void DropItem()
    {
        if (_heldItem == null)
        {
            Debug.Log("No item to drop.");
            return;
        }

        if (_heldItem.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }
        _heldItem.GetComponent<Collider>().enabled = true;

        _heldItem.transform.SetParent(null);
        _heldItem.transform.position = transform.position + transform.forward * 1.5f;

        if (_heldItem.CompareTag("Key"))
        {
            _isHoldingKey = false;
        }

        _heldItem = null;
        Debug.Log("Dropped item.");
    }

    void TryOpenDoor()
    {
        if (door != null)
        {
            if (door.CompareTag("LockedDoor") && _isHoldingKey)
            {
                door.Open();
                Destroy(_heldItem); // Destroy the key after opening the door
                _heldItem = null;
                _isHoldingKey = false;
                Debug.Log("Locked door opened, key destroyed.");
            }
            else if (door.CompareTag("UnlockedDoor"))
            {
                door.Open();
                Debug.Log("Unlocked door opened.");
            }
            else
            {
                Debug.Log("You need a key to open this door!");
            }
        }
        else
        {
            Debug.Log("No door found.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _pickupRadius);
    }
}