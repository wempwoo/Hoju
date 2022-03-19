using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Debug.Log($"{this.GetInstanceID()}: damage[{point}], HP[{hp}]");

            this.hp -= point;

            if (this.hp <= 0)
            {
                this.Destroy();
            }
        }

        public static IEnumerable<Enemy> GetEnemyEntities()
        {
            return GameObject.FindGameObjectsWithTag("Enemy")
                .Select(obj => obj.GetComponent<Enemy>());
        }
    }
}
