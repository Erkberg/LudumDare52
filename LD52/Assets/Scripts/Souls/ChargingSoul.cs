using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingSoul : Soul
{
    protected override void Move()
    {
        base.Move();

        if(rb.velocity.magnitude < 0.1f)
        {
            Vector3 dir = player.position - transform.position;
            dir.y = 0f;
            rb.velocity = dir.normalized * moveSpeed;
        }
    }
}
