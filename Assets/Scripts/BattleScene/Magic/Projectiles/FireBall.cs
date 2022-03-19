using System;

namespace BattleScene
{
    public class FireBall : ProjectileBehavior
    {
        public override Seconds Lifespan => throw new NotImplementedException();

        public override ArcDegree Dispersion => throw new NotImplementedException();

        public override void OnHit(Projectile projectile, Enemy enemy)
        {
            throw new NotImplementedException();
        }
    }

    public class FireBallOrb : SpellOrb
    {
        public FireBallOrb() : base(new FireBall()) { }
    }
}