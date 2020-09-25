using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Camera cam;

    public LayerMask interactionMask;
    public Interactable focus;

    [Header("Player Stats")]
    [SerializeField] float speed = 12f;
    [SerializeField] float jumpHeight = 5f;

    [Header("Physics")]
    [SerializeField] float gravity = -9.81f;
    [Tooltip("Ground Distance for Sphere-Check")] [SerializeField] public float groundDistance = 0.4f;
    [Tooltip("Ground Mask for Sphere-Check")] public LayerMask groundMask;
    [Tooltip("Ground Position Object")] public Transform groundCheck;

    Vector3 velocity;
    bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        CheckGround();
        UpdateVelocity();

        //Get Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Move
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            AddJumpVelocity();
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactionMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (interactable.CanInteract(transform))
                {
                    Debug.Log("Interaction available");
                    SetFocus(interactable);
                }
            }
        }
        else
        {
            RemoveFocus();
        }

        if (Input.GetMouseButtonDown(1) && focus != null)
        {
            focus.StartInteraction();
        }

        AddGravityVelocity();
        ApplyVelocity();
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();

            focus = null;
        }
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
