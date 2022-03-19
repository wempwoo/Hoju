using System;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    public class FireBall : ProjectileBehavior
    {
        public override Seconds Lifespan => new Seconds(3);

        public override ArcDegree Dispersion => new ArcDegree(3);

        protected override void SetupExtends(DriveContext context, Projectile projectile)
        {
            var sprite = projectile.GetComponent<SpriteRenderer>();
            sprite.color = new Color(1.0f, 0.4f, 0.2f);
        }


        private readonly GameObject effectAreaPrefab = Prefabs.Load("EffectAreaPrefab");

        public override void OnHit(Projectile projectile, Enemy enemy)
        {
            float range = 2.0f;

            var effectArea = Prefabs.Instantiate<EffectArea>(this.effectAreaPrefab);
            effectArea.Color = new Color(1.0f, 0.5f, 0.2f, 0.5f);
            effectArea.Radius = range;
            effectArea.Lifespan = new Seconds(0.5f);
            effectArea.Position = projectile.Position;

            var targetEnemies = Enemy.GetEnemyEntities()
                .Where(e => Math.Abs(Vector2.Distance(e.Position, projectile.Position)) < range);

            foreach (var target in targetEnemies)
            {
                target.Damaged(30);
            }
        }
    }

    public class FireBallOrb : SpellOrb
    {
        public FireBallOrb() : base(new FireBall()) { }
    }
}