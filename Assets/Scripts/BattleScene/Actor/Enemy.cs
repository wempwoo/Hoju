using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    public class Enemy : ActorBase
    {
        private ManaCircuit circuit;

        protected override void PreStart()
        {
            this.circuit = ManaCircuit.Sample2();
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

            var context = new DriveContext(Seconds.Delta, this, this.CalcTarget());
            this.circuit.Update(context);
        }

        private ArcDegree CalcTarget()
        {
            (Manaroid nearest, float _) = Entity.GetNearest(Manaroid.FindEntities(), this.Position);
            return ArcDegree.Of(nearest.Position - this.Position);
        }

        public static IEnumerable<Enemy> GetEnemyEntities()
        {
            return GameObject.FindObjectsOfType<Enemy>();
        }
    }
}
