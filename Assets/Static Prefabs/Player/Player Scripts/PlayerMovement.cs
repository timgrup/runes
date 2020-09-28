using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float speed = 12f;
    [SerializeField] float jumpHeight = 5f;

    [Header("Physics")]
    [SerializeField] float gravity = -9.81f;
    [Tooltip("Ground Distance for Sphere-Check")] [SerializeField] public float groundDistance = 0.4f;
    [Tooltip("Ground Mask for Sphere-Check")] public LayerMask groundMask;
    [Tooltip("Ground Position Object")] public Transform groundCheck;

    CharacterController controller;

    Vector3 velocity;
    bool isGrounded;
    bool jump;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        UpdateVelocity();
        controller.Move(moveDirection * speed * Time.deltaTime);
        if (jump && isGrounded)
        {
            jump = false;
            AddJumpVelocity();
        }
        AddGravityVelocity();
        ApplyVelocity();
    }

    public void Move(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public void Jump()
    {
        jump = true;
    }

    /* 
     Sphere checks, if Player collides with Ground
     */
    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    /*
     Resets velocity if grounded
     */
    private void UpdateVelocity()
    {
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    /*
     Adds velocity, that makes player jump
     */
    private void AddJumpVelocity()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    /*
     Adds velocity, based on gravity
     */
    void AddGravityVelocity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    /*
     Applies velocity on Player, that makes him move on the Y-Axis
     */
    private void ApplyVelocity()
    {
        controller.Move(velocity * Time.deltaTime);
    }
}
