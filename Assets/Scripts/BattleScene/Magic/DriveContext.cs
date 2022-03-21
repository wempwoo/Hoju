using System;
using UnityEngine;

namespace BattleScene
{
    public class DriveContext
    {
        public readonly Seconds deltaTime;
        public readonly Entity owner;
        public readonly ArcDegree firingAngle;

        public DriveContext(Seconds deltaTime, Entity owner, ArcDegree target)
        {
            this.deltaTime = deltaTime;
            this.owner = owner;
            this.firingAngle = target;
        }
    }
}