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
            if (collision.gameObject.CompareTag("Actor"))
            {
                this.Destroy();

                var target = collision.GetComponent<ActorBase>();
                behavior.OnHit(this, target);
            }
        }
    }
}