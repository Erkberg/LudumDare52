using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolArea : MonoBehaviour
{
    public Tool tool;
    public Collider coll;

    public void SetCollEnabled(bool enabled)
    {
        coll.enabled = enabled;
    }
}
