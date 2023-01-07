using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    public void OnEssenceEnter(float expValue)
    {
        Game.inst.progress.OnExpPickup(expValue);
    }
}
