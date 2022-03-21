using System;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// マナの矢
    /// </summary>
    public class ManaBolt : ProjectileBehavior
    {
        public override Seconds Lifespan => new Seconds(3);

        public override ArcDegree Dispersion => new ArcDegree(8);

        protected override void SetupExtends(DriveContext context, Projectile projectile)
        {
            var sprite = projectile.GetComponent<SpriteRenderer>();
            sprite.color = new Color(0.8f, 0.5f, 1.0f);

            projectile.transform.localScale = new Vector2(0.1f, 0.1f);
            this.speed = 14;
        }

        public override void OnHit(Projectile projectile, ActorBase target)
        {
            target.Damaged(30);
        }

    }

    public class ManaBoltOrb : SpellOrb
    {
        public ManaBoltOrb() : base(new ManaBolt()) { }
    }
}