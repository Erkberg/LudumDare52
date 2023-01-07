using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController pc;
    public Rigidbody rb;
    public Transform cam;
    public Transform minimapCam;

    public float maxDashDuration = 0.33f;
    public float dashMultiplier = 3.33f;
    public float maxVertAngle = 80f;

    private GameInput input;
    private float vertRot = 0f;
    private float horRot = 0f;
    private bool isDashing;
    private float dashDurationPassed;

    private void Awake()
    {
        input = Game.inst.input;
    }

    private void Update()
    {
        if(pc.IsControllable())
        {
            CheckDash();
            Move();
            Look();
        }
    }

    private void Move()
    {
        Vector3 movement = cam.forward * input.GetMove().y + cam.right * input.GetMove().x;
        movement.y = Physics.gravity.y;
        movement *= pc.stats.GetMoveSpeed();
        if(isDashing)
        {
            movement *= dashMultiplier;
        }
        rb.velocity = movement;
    }

    private void Look()
    {
        horRot += input.GetLook().x;
        vertRot -= input.GetLook().y;
        vertRot = Mathf.Clamp(vertRot, -maxVertAngle, maxVertAngle);

        cam.localRotation = Quaternion.Slerp(cam.localRotation, Quaternion.Euler(vertRot, horRot, 0f), pc.stats.GetLookSpeed() * Time.deltaTime);
        minimapCam.localRotation = Quaternion.Slerp(minimapCam.localRotation, Quaternion.Euler(90f, horRot, 0f), pc.stats.GetLookSpeed() * Time.deltaTime);
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
}
