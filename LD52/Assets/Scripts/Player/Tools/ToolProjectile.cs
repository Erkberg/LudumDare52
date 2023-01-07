using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolProjectile : MonoBehaviour
{
    public Rigidbody rb;
    public Tool tool;

    private ToolLevelData data;
    private int pierceLeft;

    public void Init(Vector3 dir, ToolLevelData data, Tool tool)
    {
        this.data = data;
        pierceLeft = data.pierce;
        this.tool = tool;

        transform.SetScale(data.size);
        rb.velocity = dir.normalized * data.speed;
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
}
