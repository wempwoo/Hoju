using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 魔導機
    /// </summary>
    public class Manaroid : ActorBase
    {
        public ManaCircuit circuit;
        private readonly Prefab barGaugePrefab = new Prefab("BarGaugePrefab");

        protected override void PreStart()
        {
            this.hp = 500;

            var gauge = barGaugePrefab.Instantiate<BarGauge>();
            gauge.Position = this.Position - new Vector2(0, 0.5f);
        }

        protected override void PostUpdate()
        {
            var driveContext = new DriveContext(Seconds.Delta, this, CalcTarget());
            circuit.Update(driveContext);
        }

        private ArcDegree CalcTarget()
        {
            var enemies = Enemy.GetEnemyEntities();

            if (enemies.Count() == 0)
            {
                return ArcDegree.Top;
            }
            else
            {
                (Enemy nearest, float _) = Entity.GetNearest(enemies, this.Position);
                return ArcDegree.Of(nearest.Position - this.Position);
            }
        }

        public static IEnumerable<Manaroid> FindEntities()
        {
            return GameObject.FindObjectsOfType<Manaroid>();
        }
    }
}
