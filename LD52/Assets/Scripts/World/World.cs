using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public WorldTile worldTilePrefab;
    public int tilesAmount;
    public Vector2 levelBounds;
    public Vector3 minTileScale, maxTileScale;

    private List<WorldTile> tiles = new List<WorldTile>();

    private void Awake()
    {
        CreateWorld();
    }

    public void CreateWorld()
    {
        for (int i = 0; i < tilesAmount; i++)
        {
            CreateTile();
        }
    }

    private void CreateTile()
    {
        WorldTile tile = Instantiate(worldTilePrefab, GetRandomPosition(), Quaternion.identity, transform);
        float maxScaleY = Random.Range(minTileScale.y, maxTileScale.y);
        float scaleY = Random.Range(minTileScale.y, maxScaleY);
        tile.transform.localScale = new Vector3(Random.Range(minTileScale.x, maxTileScale.x), scaleY, Random.Range(minTileScale.z, maxTileScale.z));
        tile.maxScaleY = maxScaleY;
        tile.scaleSpeed = Random.Range(0.0133f, 0.067f) * (maxScaleY - scaleY);
        tiles.Add(tile);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-levelBounds.x, levelBounds.x), 0f, Random.Range(-levelBounds.x, levelBounds.x));
    }
}
