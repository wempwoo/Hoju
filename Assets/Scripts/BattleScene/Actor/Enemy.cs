using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    public class Enemy : ActorBase
    {
        protected override void PreStart()
        {
            this.Velocity = new Vector2(0, -1);
        }


        protected override void PreUpdate()
        {
            if (this.IsOutside)
            {
                this.Destroy();
            }
        }

        protected override void PostUpdate()
        {
            if (this.Position.y < 1 + Random.Range(0, 1))
            {
                this.Velocity = Vector2.zero;
            }
        }

        public static IEnumerable<Enemy> GetEnemyEntities()
        {
            return GameObject.FindObjectsOfType<Enemy>();
        }
    }
}
