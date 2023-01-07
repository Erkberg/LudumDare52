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

    public float maxVertAngle = 80f;

    private GameInput input;
    private float vertRot = 0f;
    private float horRot = 0f;

    private void Awake()
    {
        input = Game.inst.input;
    }

    private void Update()
    {
        if(pc.IsControllable())
        {
            Move();
            Look();
        }
    }

    private void Move()
    {
        Vector3 movement = cam.forward * input.GetMove().y + cam.right * input.GetMove().x;
        movement.y = Physics.gravity.y;
        movement *= pc.stats.GetMoveSpeed();
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
}
