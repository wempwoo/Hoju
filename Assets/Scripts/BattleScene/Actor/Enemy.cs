using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    public class Enemy : Entity
    {
        private Color defaultColor;
        void Start()
        {
            this.Velocity = new Vector2(0, -1);
            this.defaultColor = this.Renderer.color;
        }

        private bool isDamageEffecting = false;
        private Seconds damageEffectElapsed;

        void Update()
        {
            if (this.IsOutside)
            {
                this.Destroy();
            }

            if (this.isDamageEffecting)
            {
                this.Renderer.color = new Color(1, 1, 1);

                this.damageEffectElapsed += Seconds.Delta;
                if (this.damageEffectElapsed > new Seconds(0.1f))
                {
                    this.isDamageEffecting = false;
                    this.Renderer.color = this.defaultColor;
                }
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

            this.isDamageEffecting = true;
            this.damageEffectElapsed = new Seconds(0);
        }

        public static IEnumerable<Enemy> GetEnemyEntities()
        {
            return GameObject.FindGameObjectsWithTag("Enemy")
                .Select(obj => obj.GetComponent<Enemy>());
        }
    }
}
