using System;
using UnityEngine;

namespace BattleScene
{
    public class ManaBolt : ProjectileBehavior
    {
        public override Seconds Lifespan => new Seconds(3);

        public override ArcDegree Dispersion => new ArcDegree(8);

        protected override void SetupExtends(DriveContext context, Projectile projectile)
        {
            var sprite = projectile.GetComponent<SpriteRenderer>();
            sprite.color = new Color(0.8f, 0.5f, 1.0f);
        }

        public override void OnHit(Enemy enemy)
        {
            enemy.Damaged(30);
        }

    }

}