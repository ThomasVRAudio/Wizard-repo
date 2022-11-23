using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : Wizard, ITargettable
{
    public float RotationSpeed = 240.0f;
    private float gravity = 20.0f;
    private Vector3 moveDir = Vector3.zero;
    public Transform Target => transform;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v < 0) v = 0;

        transform.Rotate(0, h * RotationSpeed * Time.deltaTime, 0);

        if (characterController.isGrounded)
        {
            bool move = (v > 0.5);

            animator.SetBool("run", move);
            moveDir = Vector3.forward * v;
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
        }

        moveDir.y -= gravity * Time.deltaTime;
        characterController.Move(moveDir * Time.deltaTime);
    }
}
