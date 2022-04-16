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

            projectile.transform.localScale = new Vector2(0.2f, 0.2f);
            this.speed = 7;
        }


        private readonly Prefab effectAreaPrefab = new Prefab("BattleScene/EffectAreaPrefab");

        public override void OnHit(Projectile projectile, ActorBase target)
        {
            var effectArea = this.effectAreaPrefab.Instantiate<EffectArea>();
            effectArea.Position = projectile.Position;
            effectArea.Color = new Color(1.0f, 0.5f, 0.2f, 0.5f);
            effectArea.Radius = 1.3f;
            effectArea.Lifespan = new Seconds(0.5f);
            effectArea.EffectInterval = new Seconds(1);
            effectArea.Damage = 30;
        }
    }

    public class FireBallOrb : SpellOrb
    {
        public FireBallOrb() : base(new FireBall()) { }
    }
}