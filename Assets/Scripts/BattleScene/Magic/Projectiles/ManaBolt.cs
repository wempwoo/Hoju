using System;
namespace BattleScene
{
    public class ManaBolt : ProjectileBehavior
    {
        public override Seconds Lifespan => new Seconds(3);

        public override ArcDegree Dispersion => new ArcDegree(8);

        protected override void SetupExtends(DriveContext context, Projectile projectile)
        {
        }

        public override void OnHit(Enemy enemy)
        {
            enemy.Damaged(30);
        }

    }

}