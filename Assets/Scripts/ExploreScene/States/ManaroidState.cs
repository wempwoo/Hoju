using System;

namespace ExploreScene
{
    public class ManaroidState
    {
        /// <summary>
        /// 名前
        /// </summary>
        public readonly Reactive<string> name = new Reactive<string>();

        /// <summary>
        /// HP
        /// </summary>
        public readonly Reactive<LimitedValue<int>> hp = new Reactive<LimitedValue<int>>();

        /// <summary>
        /// マナ充填量
        /// </summary>
        public readonly Reactive<LimitedValue<int>> manaCharge = new Reactive<LimitedValue<int>>();

    }
}