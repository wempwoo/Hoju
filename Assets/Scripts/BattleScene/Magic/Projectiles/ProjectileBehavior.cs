using System.Linq;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 投射魔法の振る舞いを実装するための基底クラス
    /// </summary>
    public abstract class ProjectileBehavior
    {
        protected float speed = 10.0f;

        public void Setup(DriveContext context, Projectile projectile)
        {
            projectile.behavior = this;
            projectile.Position = context.owner.Position;
            projectile.Velocity = Target(context.firingAngle);

            this.SetupExtends(context, projectile);
        }

        protected virtual void SetupExtends(DriveContext context, Projectile projectile)
        {

        }

        /// <summary>
        /// 攻撃対象へのベクトルを求める
        /// </summary>
        /// <param name="firingAngle"></param>
        /// <returns></returns>
        private Vector2 Target(ArcDegree firingAngle)
        {
            var targetVelocity = firingAngle.ToVector(this.speed);

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
        /// <param name="target"></param>
        public abstract void OnHit(Projectile projectile, ActorBase target);
    }
}
