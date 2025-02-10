using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMotion : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    [SerializeField] private Transform orienter;
    [SerializeField] private Transform camHolder;

    [Header("Movement Controls")]
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float speedMultiplier;
    [Space(5)]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    public bool grounded;

    [Header("Rotation Controls")]
    [SerializeField] private float xSens;
    [SerializeField] private float ySens;
    [SerializeField] private float sensMultiplier;
    private float xRot;
    private float yRot;
    [Space(5)]
    [SerializeField] private float minLook;
    [SerializeField] private float maxLook;

    private PlayerInputHandler ih;

    private Vector3 moveDir;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ih = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Move Input: " + ih.MoveInput + ", " + "Sprint Value: " + ih.SprintValue + 
            ", " + "Move Speed: " + moveSpeed + " | " + "Look Input: " + ih.LookInput + ", "
            + "x Rot: " + xRot);
        HandleRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveDir = orienter.forward * ih.MoveInput.y + orienter.right * ih.MoveInput.x;

        moveSpeed = ih.SprintValue > 0 && ih.MoveInput.y > 0 ? walkSpeed + ih.SprintValue * (runSpeed - walkSpeed) : walkSpeed;

        rb.AddForce(moveDir.normalized * moveSpeed * speedMultiplier, ForceMode.Force);


        rb.drag = grounded ? groundDrag : airDrag;
    }

    private void HandleRotation()
    {
        xRot += ih.LookInput.y * ySens;
        yRot += ih.LookInput.x * xSens;

        xRot = Mathf.Clamp(xRot, minLook, maxLook);

        orienter.transform.rotation = Quaternion.Euler(0, yRot, 0);
        camHolder.rotation = Quaternion.Euler(-xRot, yRot, 0);
    }
}
