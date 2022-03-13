using System;
using System.Linq;
using UnityEngine;

public class SpellOrb
{
    private GameObject projectilePrefab;

    public SpellOrb()
    {
        projectilePrefab = Prefabs.Load("ProjectilePrefab");
    }

    public void Drive(DriveContext context)
    {
        var projectile = Prefabs.Instantiate<ProjectileBase>(projectilePrefab);
        projectile.Lifespan = new Seconds(3);
        projectile.Position = context.playerPosition;
        projectile.Velocity = CalcVelocity(context.playerPosition);
    }

    private static Vector2 CalcVelocity(Vector2 myPosition)
    {
        var nearest = GameObject.FindGameObjectsWithTag("Enemy")
            .Select(e => e.transform.position)
            .Select(p => new { pos = (Vector2)p, dist = Vector2.Distance(myPosition, p) })
            .OrderBy(t => t.dist)
            .FirstOrDefault();

        var angle = (nearest == null) ? ArcDegree.Top : ArcDegree.Of(nearest.pos - myPosition);
        return ArcDegree.ToVector(angle, 10);
    }
}
