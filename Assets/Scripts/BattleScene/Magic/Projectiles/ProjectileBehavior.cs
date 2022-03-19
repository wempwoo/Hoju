using System.Linq;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 投射魔法の振る舞いを実装するための基底クラス
    /// </summary>
    public abstract class ProjectileBehavior
    {
        public void Setup(DriveContext context, Projectile projectile)
        {
            projectile.behavior = this;
            projectile.Position = context.playerPosition;
            projectile.Velocity = Target(context.playerPosition);

            this.SetupExtends(context, projectile);
        }

        protected virtual void SetupExtends(DriveContext context, Projectile projectile)
        {

        }

        /// <summary>
        /// 攻撃対象へのベクトルを求める
        /// </summary>
        /// <param name="myPosition"></param>
        /// <returns></returns>
        private Vector2 Target(Vector2 myPosition)
        {
            var enemies = Enemy.GetEnemyEntities();

            ArcDegree angle;
            if (enemies.Count() == 0)
            {
                angle = ArcDegree.Top;
            }
            else
            {
                (Enemy _, Vector2 position, float _) = Entity.GetNearest(enemies, myPosition);
                angle = ArcDegree.Of(position - myPosition);
            }

            var targetVelocity = ArcDegree.ToVector(angle, 10);

            float dispersionHalf = Dispersion.value / 2;
            ArcDegree directionRev = new ArcDegree(Random.Range(-dispersionHalf, dispersionHalf));
            return ArcDegree.Rotate(targetVelocity, directionRev);
        }

        /// <summary>
        /// 投射物の寿命
        /// </summary>
        public abstract Seconds Lifespan { get; }

        /// <summary>
        /// 投射角度の分散
        /// </summary>
        public abstract ArcDegree Dispersion { get; }

        /// <summary>
        /// 投射物が敵にヒットしたイベント
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="enemy"></param>
        public abstract void OnHit(Projectile projectile, Enemy enemy);
    }
}
