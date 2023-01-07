using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public WorldTileTop worldTileTop;
    public float maxScaleY;
    public float scaleSpeed;

    private int scaleDir;

    private void Awake()
    {
        scaleDir = Random.value < 0.5f ? 1 : -1;
    }

    private void Update()
    {
        if(!worldTileTop.hasPlayer)
        {
            Scale();
        }        
    }

    private void Scale()
    {
        float scaleY = transform.localScale.y;
        if ((scaleY <= 1f && scaleDir == -1) || (scaleY >= maxScaleY && scaleDir == 1))
        {
            scaleDir *= -1;
        }

        transform.SetScaleY(scaleY + scaleSpeed * scaleDir * Time.deltaTime);        
    }
}
