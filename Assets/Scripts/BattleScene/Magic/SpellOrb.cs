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
        private readonly Prefab projectilePrefab = new Prefab("ProjectilePrefab");

        private readonly ProjectileBehavior spell;

        public SpellOrb(ProjectileBehavior spell)
        {
            this.spell = spell;
        }

        public void Drive(DriveContext context)
        {
            var projectile = projectilePrefab.Instantiate<Projectile>();
            projectile.owner = context.owner;
            this.spell.Setup(context, projectile);
        }

    }
}
