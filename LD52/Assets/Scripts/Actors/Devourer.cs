using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Devourer : MonoBehaviour
{
    public State state;
    public LayerMask layerMask;
    [Space]
    public Rigidbody rb;
    public Collider coll;
    public new Light light;
    public ParticleSystem particle;
    [Space]
    public AudioSource asChatter;
    public AudioSource asHunt;
    public AudioSource asDevour;
    [Space]
    public float moveSpeed = 2f;
    public float huntingSpeedMultiplier = 2f;
    public float offsetY = 2f;
    public float targetCheckInterval = 1f;
    public float huntingRange = 16f;
    public float minLightIntensity = 1f;

    private Transform currentHuntTarget;
    private float targetCheckIntervalPassed;
    private float lightIntensity;

    public enum State
    {
        Moving,
        Hunting,
        Standing
    }

    private void Awake()
    {
        lightIntensity = light.intensity;
    }

    private void Update()
    {
        HandleState();
        HandlePositionY();
        Timing.AddTimeAndCheckMax(ref targetCheckIntervalPassed, targetCheckInterval, Time.deltaTime, CheckTarget);
    }

    private void HandleState()
    {
        switch (state)
        {
            case State.Moving:
                Move();
                light.intensity = Mathf.Lerp(light.intensity, minLightIntensity, Time.deltaTime);
                break;

            case State.Hunting:
                Hunt();
                light.intensity = Mathf.Lerp(light.intensity, lightIntensity, Time.deltaTime * 3.33f);
                break;

            case State.Standing:
                if(Random.value < 0.0167f)
                {
                    state = State.Moving;
                }
                break;
        }
    }

    private void Move()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.01f || Mathf.Abs(rb.velocity.z) < 0.01f || Random.value < 0.00033f)
        {
            rb.velocity = new Vector3(Random.Range(-moveSpeed, moveSpeed), 0f, Random.Range(-moveSpeed, moveSpeed));
        }
    }

    private void Hunt()
    {
        if(currentHuntTarget)
        {
            Vector3 velo = (currentHuntTarget.position - transform.position).normalized;
            velo.y = 0f;
            rb.velocity = velo * moveSpeed * huntingSpeedMultiplier;
        }
        else
        {
            state = State.Moving;
            Debug.LogError("hunting without target");
        }
    }

    private void HandlePositionY()
    {
        bool hasHit = Physics.Raycast(transform.position + Vector3.up * 20, Vector3.down, out RaycastHit hit, 25f, layerMask);
        if (hasHit)
        {
            transform.SetPositionY(hit.point.y + offsetY);
        }
    }

    private void CheckTarget()
    {
        if(state == State.Standing)
        {
            return;
        }

        Transform closest = Game.inst.refs.GetClosestDevourerTarget(transform.position);
        if(Vector3.Distance(transform.position, closest.position) < huntingRange)
        {
            if(currentHuntTarget != closest)
            {
                currentHuntTarget = closest;
                asHunt.Play();
                state = State.Hunting;
            }            
        }
        else if(!currentHuntTarget || Random.value < 0.067f)
        {
            currentHuntTarget = null;
            state = State.Moving;
            if(Random.value < 0.133f)
            {
                Vector3 velo = (Game.inst.refs.GetRandomDevourerTarget().position - transform.position).normalized;
                velo.y = 0f;
                rb.velocity = velo * moveSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Soul soul = other.GetComponent<Soul>();
        if(soul)
        {
            asDevour.Play();
            soul.OnDevour();
            currentHuntTarget = null;
            state = State.Standing;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Soul soul = collision.collider.GetComponent<Soul>();
        if (soul)
        {
            //Debug.Log(name + " devours " + soul.name);
            asDevour.Play();
            soul.OnDevour();
            currentHuntTarget = null;
            state = State.Standing;
            rb.velocity = Vector3.zero;
        }

        PlayerBodyCC playerBody = collision.collider.GetComponent<PlayerBodyCC>();
        if(playerBody)
        {
            asDevour.Play();
            asChatter.Stop();
            playerBody.OnDevour(transform);
        }
    }

    private Vector3 GetRandomRespawnPosition()
    {
        return new Vector3(Random.Range(1f, Game.inst.refs.level.size.x), offsetY, Random.Range(1f, Game.inst.refs.level.size.y));
    }
}
