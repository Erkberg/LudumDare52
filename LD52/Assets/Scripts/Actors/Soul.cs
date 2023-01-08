using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public State state;
    [Space]
    public Rigidbody rb;
    public new Light light;
    public ParticleSystem particle;
    [Space]
    public float moveSpeed = 2f;

    private Transform currentHunter;

    public enum State
    {
        Moving,
        Hunted,
        Devoured,
        Respawning
    }

    private void Update()
    {
        HandleState();
    }

    private void HandleState()
    {
        switch (state)
        {
            case State.Moving:
                Move();
                break;

            case State.Hunted:
                FleeFrom(currentHunter);
                break;

            case State.Devoured:
                break;

            case State.Respawning:
                break;
        }
    }

    private void Move() 
    {
        if(Mathf.Abs(rb.velocity.x) < 0.01f || Mathf.Abs(rb.velocity.z) < 0.01f || Random.value < 0.001f)
        {
            rb.velocity = new Vector3(Random.Range(-moveSpeed, moveSpeed), 0f, Random.Range(-moveSpeed, moveSpeed));
        }
    }

    private void FleeFrom(Transform hunter)
    {
        if(hunter)
        {
            rb.velocity = (transform.position - hunter.position).normalized * moveSpeed;
        }
        else
        {
            Debug.LogError("no hunter but fleeing");
        }
    }

    private void HandlePositionY()
    {
        bool hasHit = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);
        if(hasHit)
        {

        }
    }
}
