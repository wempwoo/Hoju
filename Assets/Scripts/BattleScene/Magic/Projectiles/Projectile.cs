using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 魔法による投射物
    /// </summary>
    public class Projectile : Entity
    {
        public ProjectileBehavior behavior;

        private Seconds elapsed = new Seconds(0);

        public Entity owner;

        void Start()
        {
        }

        void Update()
        {
            if (this.IsOutside)
            {
                this.Destroy();
                return;
            }

            elapsed += Seconds.Delta;

            if (elapsed > behavior.Lifespan)
            {
                Destroy();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<ActorBase>();
            if (target == null || target == this.owner)
            {
                return;
            }

            this.Destroy();

            behavior.OnHit(this, target);
        }
    }
}