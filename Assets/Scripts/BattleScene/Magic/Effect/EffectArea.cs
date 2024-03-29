using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 効果領域
    /// 何らかの効果が及ぶ領域を処理する
    /// </summary>
    public class EffectArea : Entity
    {
        /// <summary>
        /// 領域の寿命
        /// </summary>
        public Seconds Lifespan { get; set; }

        /// <summary>
        /// 領域の半径
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// 領域の色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// ダメージ量
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// 効果の発生間隔
        /// </summary>
        public Seconds EffectInterval { get; set; } = new Seconds(1);

        void Start()
        {
            this.Renderer.transform.localScale = new Vector2(this.Radius * 2, this.Radius * 2);
            this.Renderer.color = this.Color;
        }

        private Seconds elapsed = new Seconds(0);

        /// <summary>
        /// 効果適用やインターバルは敵インスタンスごとに管理する
        /// キーはインスタンスID、値は前回適用時からの経過時間
        /// </summary>
        private readonly Dictionary<int, Seconds> effecteds = new Dictionary<int, Seconds>();

        void Update()
        {
            Seconds delta = Seconds.Delta;
            this.elapsed += delta;

            if (this.elapsed > this.Lifespan)
            {
                this.Destroy();
            }

            var targetEnemies = Enemy.GetEnemyEntities()
                .Where(e => Math.Abs(Vector2.Distance(e.Position, this.Position)) < this.Radius);

            foreach (var target in targetEnemies)
            {
                int id = target.GetInstanceID();

                // 初検出の敵
                if (!this.effecteds.ContainsKey(id))
                {
                    // 下のif文で確実に処理される経過時間をセットしておく
                    this.effecteds[id] = this.EffectInterval;
                }

                this.effecteds[id] += delta;

                if (this.effecteds[id] > this.EffectInterval)
                {
                    this.effecteds[id] = new Seconds(0);
                    target.Damaged(this.Damage);
                }
            }
        }
    }
}