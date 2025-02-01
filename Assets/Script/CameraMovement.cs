using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Collision")]
    public Transform player;
    public float cameraHeight = 1.6f;
    public float smoothSpeed = 10f;
    public LayerMask collisionMask;
    public float cameraRadius = 0.3f;
    private Vector3 desiredPosition;

    [Header("Camera Movement")]
    [SerializeField]
    private float cameraSensitivity = 2f;
    [SerializeField]
    private Transform playerBody;
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Vector3 cameraOffset = player.forward * cameraRadius + Vector3.up * cameraHeight;
        desiredPosition = player.position + cameraOffset;

        RaycastHit hit;
        if (Physics.SphereCast(
            player.position + Vector3.up * cameraHeight, 
            cameraRadius, 
            (desiredPosition - (player.position + Vector3.up * cameraHeight)).normalized, 
            out hit, 
            Vector3.Distance(player.position + Vector3.up * cameraHeight, desiredPosition), 
            collisionMask))
        {
            desiredPosition = hit.point - (hit.normal * cameraRadius);
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    void Update()
    {
        // Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
