using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringSoul : Soul
{
    protected override void Move()
    {
        base.Move();

        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        rb.velocity = dir.normalized * moveSpeed;
    }
}
