using UnityEngine;
using System.Collections;
using System;

/** 
 * Copyright (C) 2016 - Peter Wages
 **/

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float crouchSpeed = 3.0f;
    public float gravity = -6f;
    public float jumpSpeed = 200f;
    public float standingHeightSameAsStartScaleYInEditor = 0.6f;
    public float crouchHeight = 0.3f;
    
    private GameObject character;
    private CharacterController characterController;
    private float jumpSmoothness = .2f;
    private float crouchSmoothness = .1f;
    private bool crouching = false;
    private bool jumpCompleted = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        character = this.gameObject;
    }

    // Calculates player's movement
    void Update()
    {
        float deltaX = PlayerSpeed(Input.GetAxis("Horizontal"));
        float deltaZ = PlayerSpeed(Input.GetAxis("Vertical"));
        float vertSpeed = 0;

        vertSpeed = Jump(vertSpeed);
        Crouch();

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    // Calculates and returns player's speed, accounting for crouching
    private float PlayerSpeed(float rawSpeed)
    {
        if (crouching)
        {
            return rawSpeed * crouchSpeed;
        } else
        {
            return rawSpeed * speed;
        }
    }

    // Checks if player is crouching
    private void Crouch()
    {
        if ((Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftShift)) && characterController.isGrounded)
        {
            crouching = true;
            ScaleForCrouching(crouchHeight);
        } else
        {
            crouching = false;
            ScaleForCrouching(standingHeightSameAsStartScaleYInEditor);
        }
    }

    // Scales the character for crouching
    private void ScaleForCrouching(float height)
    {
        Vector3 scale = character.transform.localScale;
        scale.y = Mathf.Lerp(scale.y, height, crouchSmoothness);
        character.transform.localScale = scale;
    }

    // Makes player jump
    private float Jump(float vertSpeed)
    {
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed += gravity;
            }
        }
        else
        {
            vertSpeed += gravity;
        }
        return vertSpeed;
    }
}
