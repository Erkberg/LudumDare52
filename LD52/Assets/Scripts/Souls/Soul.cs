using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public Id id;
    public State state;
    public Rigidbody rb;
    public MeshRenderer mesh;
    public MeshRenderer essence;
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    protected Transform player;

    public enum Id
    {
        None,
        Hovering,
        Charging, 
        Circling, 
        Flying, 
        Jumping,
        Spawning
    }

    public enum State
    {
        None,
        Spawning,
        Moving,
        Essence
    }

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        player = Game.inst.refs.player.body.transform;
        ChangeState(State.Moving);
    }

    protected void ChangeState(State state)
    {
        this.state = state;
    }

    private void Update()
    {
        CheckState();
    }

    protected virtual void CheckState()
    {
        switch (state)
        {
            case State.None:
                break;

            case State.Spawning:
                break;

            case State.Moving:
                Move();
                break;

            case State.Essence:
                mesh.gameObject.SetActive(false);
                essence.gameObject.SetActive(true);
                break;
        }
    }

    protected virtual void Move() { }

    protected virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0f)
        {
            ChangeState(State.Essence);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBody playerBody = other.GetComponent<PlayerBody>();
        if(playerBody && state == State.Moving)
        {
            playerBody.OnSoulEnter();
        }

        PlayerMagnet playerMagnet = other.GetComponent<PlayerMagnet>();
        if (playerMagnet && state == State.Essence)
        {
            playerMagnet.OnEssenceEnter();
        }

        ToolArea toolArea = other.GetComponent<ToolArea>();
        {
            if(toolArea && state == State.Moving)
            {
                TakeDamage(toolArea.tool.GetDamage());
            }
        }
    }
}
