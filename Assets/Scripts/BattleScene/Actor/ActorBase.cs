using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    public abstract class ActorBase : Entity
    {
        public ActorBase()
        {
        }

        private Color defaultColor;

        void Start()
        {
            this.PreStart();

            this.defaultColor = this.Renderer.color;

            this.PostStart();
        }

        protected virtual void PreStart() { }
        protected virtual void PostStart() { }

        void Update()
        {
            this.PreUpdate();

            this.ProcessDamage();

            this.PostUpdate();
        }

        private void ProcessDamage()
        {
            if (!this.isDamageEffecting) return;

            this.Renderer.color = new Color(1, 1, 1);

            this.damageEffectElapsed += Seconds.Delta;
            if (this.damageEffectElapsed > new Seconds(0.1f))
            {
                this.isDamageEffecting = false;
                this.Renderer.color = this.defaultColor;
            }
        }

        protected virtual void PreUpdate() { }
        protected virtual void PostUpdate() { }

        private bool isDamageEffecting = false;
        private Seconds damageEffectElapsed;
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

    }
}