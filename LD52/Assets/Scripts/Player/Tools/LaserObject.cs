using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour
{
    public ToolArea toolArea;
    public ParticleSystem particleBig;
    public ParticleSystem particleSmall;

    public void SetParticleRadius(float value)
    {
        var shape = particleBig.shape;
        shape.radius = value;
    }

    public void SetBig(bool big)
    {
        particleBig.gameObject.SetActive(big);
        particleSmall.gameObject.SetActive(!big);
    }
}
