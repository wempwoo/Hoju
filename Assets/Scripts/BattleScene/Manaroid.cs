using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 魔導機
    /// </summary>
    public class Manaroid : Entity
    {
        public ManaCircuit circuit;

        void Start()
        {
        }

        void Update()
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
    }
}
