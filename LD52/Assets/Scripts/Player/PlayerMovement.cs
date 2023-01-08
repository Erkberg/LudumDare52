using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController pc;
    public Rigidbody rb;
    public CharacterController cc;
    public Transform cam;
    public Transform minimapCam;

    public float moveSpeed = 4f;
    public float lookSpeed = 33f;
    public bool jumpEnabled;
    public float jumpStrength = 8f;
    public bool dashEnabled;
    public float maxDashDuration = 0.33f;
    public float dashMultiplier = 3.33f;
    public bool runEnabled;
    public float maxrunDuration = 6.67f;
    public float runMultiplier = 3.33f;
    public float maxVertAngle = 80f;

    private GameInput input;
    private float vertRot = 0f;
    private float horRot = 0f;
    private bool isDashing;
    private bool isRunning;
    private float dashDurationPassed;
    private bool doubleJumpAvailable;

    private void Awake()
    {
        input = Game.inst.input;
    }

    private void Update()
    {
        if(pc.IsControllable())
        {
            if(dashEnabled)
            {
                CheckDash();
            }

            if(runEnabled)
            {
                CheckRun();
            }
            
            Move();            
            Look();

            if(jumpEnabled)
            {
                CheckJump();
                if (IsGrounded())
                {
                    doubleJumpAvailable = true;
                }
            }            
        }
    }

    private void Move()
    {
        Vector3 movement = cam.forward * input.GetMove().y + cam.right * input.GetMove().x;        
        movement *= moveSpeed;        
        if (isDashing)
        {
            movement *= dashMultiplier;
        }

        if(rb)
        {
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }
        else if(cc)
        {
            movement.y = Physics.gravity.y;
            cc.Move(movement * Time.deltaTime);
        }
    }

    private void Look()
    {
        horRot += input.GetLook().x;
        vertRot -= input.GetLook().y;
        vertRot = Mathf.Clamp(vertRot, -maxVertAngle, maxVertAngle);

        cam.localRotation = Quaternion.Slerp(cam.localRotation, Quaternion.Euler(vertRot, horRot, 0f), lookSpeed * Time.deltaTime);
        if(minimapCam)
        {
            minimapCam.localRotation = Quaternion.Slerp(minimapCam.localRotation, Quaternion.Euler(90f, horRot, 0f), lookSpeed * Time.deltaTime);
        }        
    }

    private void CheckJump()
    {
        if (input.GetJumpDown())
        {
            if(IsGrounded())
            {
                rb.SetVelocityY(jumpStrength);
            }
            else if (doubleJumpAvailable)
            {
                doubleJumpAvailable = false;
                rb.SetVelocityY(jumpStrength);
            }
        }        
    }

    private void CheckDash()
    {
        if(isDashing)
        {
            if(input.GetDashUp() || dashDurationPassed >= maxDashDuration)
            {
                isDashing = false;
            }
            else
            {
                dashDurationPassed += Time.deltaTime;
            }
        }
        else if(input.GetDashDown())
        {
            dashDurationPassed = 0f;
            isDashing = true;
        }
    }

    private void CheckRun()
    {
        if (isRunning)
        {
            if (input.GetDashUp() || dashDurationPassed >= maxDashDuration)
            {
                isDashing = false;
            }
            else
            {
                dashDurationPassed += Time.deltaTime;
            }
        }
        else if (input.GetDashDown())
        {
            dashDurationPassed = 0f;
            isDashing = true;
        }
    }

    public bool IsGrounded()
    {
        return pc.body.HasCurrentTile();
    }
}
