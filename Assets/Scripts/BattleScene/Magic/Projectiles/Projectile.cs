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

            elapsed += new Seconds(Time.deltaTime);

            if (elapsed > behavior.Lifespan)
            {
                Destroy();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                this.Destroy();

                var enemy = collision.GetComponent<Enemy>();
                behavior.OnHit(this, enemy);
            }
        }
    }
}