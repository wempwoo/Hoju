using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 魔導機
    /// </summary>
    public class Manaroid : Entity
    {
        public MagicCircuit circuit;

        void Start()
        {
        }

        void Update()
        {
            var driveContext = new DriveContext(this.Position, Seconds.Delta);
            circuit.Update(driveContext);
        }
    }
}
