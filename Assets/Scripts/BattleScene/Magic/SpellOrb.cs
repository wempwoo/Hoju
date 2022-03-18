using System;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    public class SpellOrb
    {
        private GameObject projectilePrefab;

        public SpellOrb()
        {
            projectilePrefab = Prefabs.Load("ProjectilePrefab");
        }

        public void Drive(DriveContext context)
        {
            var projectile = Prefabs.Instantiate<Projectile>(projectilePrefab);
            projectile.behavior = new ManaBolt(context, projectile);
        }

    }
}
