using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class Enemy : Entity
    {
        void Start()
        {
            this.Velocity = new Vector2(0, -1);
        }

        void Update()
        {
            if (this.IsOutside)
            {
                this.Destroy();
            }

            if (this.Position.y < 1 + Random.Range(0, 1))
            {
                this.Velocity = Vector2.zero;
            }
        }

        private int hp = 100;

        public void Damaged(int point)
        {
            this.hp -= point;

            if (this.hp <= 0)
            {
                this.Destroy();
            }
        }
    }
}
