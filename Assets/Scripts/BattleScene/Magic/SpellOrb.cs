using System;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 術式宝珠
    /// </summary>
    public abstract class SpellOrb
    {
        private readonly GameObject projectilePrefab;

        private readonly ProjectileBehavior spell;

        public SpellOrb(ProjectileBehavior spell)
        {
            this.projectilePrefab = Prefabs.Load("ProjectilePrefab");
            this.spell = spell;
        }

        public void Drive(DriveContext context)
        {
            var projectile = Prefabs.Instantiate<Projectile>(projectilePrefab);
            this.spell.Setup(context, projectile);
        }

    }
}
