using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTool : Tool
{
    protected override void OnCooldown()
    {
        base.OnCooldown();

        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        ToolProjectile projectile = Instantiate(projectilePrefab, tools.muzzle.position, tools.muzzle.rotation, Game.inst.refs.projectilesHolder);
        projectile.Init(tools.muzzle.forward, GetData(), this);
    }
}
