using System.Runtime.CompilerServices;
using UnityEngine;

public class RustMovement : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    [SerializeField] private Transform orienter;
    [SerializeField] private Transform objectsHolder;

    [Header("Movement Controls")]
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float speedMultiplier;
    [Space(5)]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    public bool grounded;
    [Space(5)]
    [SerializeField] private float jumpForce;
    private bool jump;

    [Header("Rotation Controls")]
    [SerializeField] private float xSens;
    [SerializeField] private float ySens;
    [SerializeField] private float sensMultiplier;
    private float xRot;
    private float yRot;
    [Space(5)]
    [SerializeField] private float minLook;
    [SerializeField] private float maxLook;
    [SerializeField] private Vector3 correction;

    private PlayerInputHandler ih;

    private Vector3 moveDir;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ih = GetComponent<PlayerInputHandler>();

        jump = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Move Input: " + ih.MoveInput + ", " + "Sprint Value: " + ih.SprintValue +
            ", " + "Move Speed: " + moveSpeed + " | " + "Look Input: " + ih.LookInput + ", "
            + "x Rot: " + xRot);
        HandleRotation();
        HandleJump();

        grounded = Input.GetKeyDown(KeyCode.G) ? !grounded : grounded;

        moveDir = orienter.forward * ih.MoveInput.y + orienter.right * ih.MoveInput.x;

        moveSpeed = ih.SprintValue > 0 && ih.MoveInput.y > 0 ? walkSpeed + ih.SprintValue * (runSpeed - walkSpeed) : walkSpeed;
        rb.linearDamping = grounded ? groundDrag : airDrag;

    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDir.normalized * moveSpeed * speedMultiplier, ForceMode.Force);

        if (jump)
        {
            jump = false;
            rb.AddForce(orienter.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleJump()
    {
        if (ih.JumpTriggered)
        {
            jump = true;
            ih.JumpTriggered = false;
        }
    }

    private void HandleRotation()
    {
        xRot += ih.LookInput.y * ySens * Time.deltaTime;
        yRot += ih.LookInput.x * xSens * Time.deltaTime;

        xRot = Mathf.Clamp(xRot, minLook, maxLook);

        orienter.transform.rotation = Quaternion.Euler(0, yRot, 0);
        objectsHolder.rotation = Quaternion.Euler(-xRot+correction.x, yRot+correction.y, 0+correction.z);
    }
}
