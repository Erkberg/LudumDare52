using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolProjectile : MonoBehaviour
{
    public Rigidbody rb;
    public Tool tool;
    public ParticleSystem particle;
    public ParticleSystem destroyParticle;

    private ToolLevelData data;
    private int pierceLeft;
    private bool focused;

    public void Init(Vector3 dir, ToolLevelData data, Tool tool)
    {
        this.data = data;
        pierceLeft = data.pierce;
        this.tool = tool;
        focused = Game.inst.refs.player.focus.IsFocussing();

        transform.SetScale(data.size);
        rb.velocity = dir.normalized * data.speed * (focused ? 3.33f : 1f);
        Destroy(gameObject, data.range);
    }

    public void OnEnteredSoul()
    {
        if(pierceLeft > 0)
        {
            pierceLeft--;
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    public float GetDamage()
    {
        return data.damage;
    }

    private void OnDestroy()
    {
        destroyParticle.Emit(32);
    }
}
