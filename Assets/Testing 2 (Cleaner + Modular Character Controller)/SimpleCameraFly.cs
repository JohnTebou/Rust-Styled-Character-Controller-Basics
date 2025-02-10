using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFly : MonoBehaviour
{
    [Header("Movement Controls")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier;
    private float targetSpeed;

    [Header("Rotation Controls")]
    [SerializeField] private float lookSens;
    [SerializeField] private float minLook;
    [SerializeField] private float maxLook;
    private float xRot;
    private float yRot;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMotion();
        HandleRotation();
    }

    void HandleMotion()
    {
        targetSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * speedMultiplier : moveSpeed;

        float xMov = Input.GetAxis("Horizontal") * targetSpeed * Time.deltaTime;
        float yMov = Input.GetAxis("Vertical") * targetSpeed * Time.deltaTime;

        transform.Translate(xMov, 0, yMov);

        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0, targetSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(0, -targetSpeed * Time.deltaTime, 0);
        }
    }

    void HandleRotation()
    {
        xRot += Input.GetAxis("Mouse X") * lookSens * Time.deltaTime;
        yRot -= Input.GetAxis("Mouse Y") * lookSens * Time.deltaTime;

        yRot = Mathf.Clamp(yRot, minLook, maxLook);

        transform.rotation = Quaternion.Euler(yRot, xRot, 0);
    }
}
