using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public State state;
    [Space]
    public Rigidbody rb;
    public Collider coll;
    public new Light light;
    public ParticleSystem particle;
    public ParticleSystem particleSalvation;
    public ParticleSystem particleDevour;
    [Space]
    public float moveSpeed = 2f;
    public float fleeSpeedMultiplier = 1.5f;
    public float offsetY = 2f;

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
        HandlePositionY();
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
            rb.velocity = (transform.position - hunter.position).normalized * moveSpeed * fleeSpeedMultiplier;
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

        transform.SetPositionY(offsetY);
    }

    public void OnSalvation()
    {
        Game.inst.state.scorePlayer++;
        particleSalvation.Emit(32);
        Respawn();
    }

    public void OnDevour()
    {
        Game.inst.state.scoreDevourer++;
        particleDevour.Emit(32);
        Respawn();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence()
    {
        rb.velocity = Vector3.zero;
        coll.enabled = false;
        particle.SetEmissionEnabled(false);
        float value = 1f;
        float lightIntensity = light.intensity;

        while(value > 0f)
        {
            value -= Time.deltaTime;
            light.intensity = lightIntensity * value;
            yield return null;
        }
        value = 0f;
        yield return null;
        transform.position = GetRandomRespawnPosition();
        HandlePositionY();
        yield return null;
        while (value < 1f)
        {
            value += Time.deltaTime;
            light.intensity = lightIntensity * value;
            yield return null;
        }
        coll.enabled = true;
        particle.SetEmissionEnabled(true);
    }

    private Vector3 GetRandomRespawnPosition()
    {
        return new Vector3(Random.Range(1f, Game.inst.refs.level.size.x), offsetY, Random.Range(1f, Game.inst.refs.level.size.y));
    }
}
