using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public PlayerController pc;

    private WorldTileTop lastTile;
    private WorldTileTop currentTile;

    private void Update()
    {
        if(transform.position.y < -3.33f)
        {
            transform.position = lastTile.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WorldTileTop worldTileTop = other.GetComponent<WorldTileTop>();
        if(worldTileTop)
        {
            lastTile = worldTileTop;
            worldTileTop.hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        WorldTileTop worldTileTop = other.GetComponent<WorldTileTop>();
        if (worldTileTop)
        {
            worldTileTop.hasPlayer = false;
            currentTile = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        WorldTileTop worldTileTop = other.GetComponent<WorldTileTop>();
        if (worldTileTop)
        {
            currentTile = worldTileTop;
        }
    }

    public bool HasCurrentTile()
    {
        return currentTile != null;
    }
}
