using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class EffectArea : Entity
    {
        public Seconds Lifespan { get; set; }

        public float Radius { get; set; }

        public Color Color { get; set; }

        void Start()
        {
            this.transform.localScale = new Vector2(this.Radius, this.Radius);
            this.Renderer.color = this.Color;
        }

        private Seconds elapsed = new Seconds(0);

        void Update()
        {
            Seconds delta = new Seconds(Time.deltaTime);
            this.elapsed += delta;

            if (this.elapsed > this.Lifespan)
            {
                this.Destroy();
            }
        }
    }
}