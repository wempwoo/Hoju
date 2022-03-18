using System;
namespace BattleScene
{
    public class ManaBolt : ProjectileBehavior
    {
        public ManaBolt(DriveContext context, Projectile projectile)
            : base(context, projectile)
        { }

        public override Seconds Lifespan => new Seconds(3);

        public override ArcDegree Dispersion => new ArcDegree(8);

        public override void OnHit(Enemy enemy)
        {
            enemy.Damaged(30);
        }

    }

}