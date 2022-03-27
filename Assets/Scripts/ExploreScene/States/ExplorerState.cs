using System;

namespace ExploreScene
{
    /// <summary>
    /// 探索者
    /// </summary>
    public class ExplorerState
    {
        /// <summary>
        /// 名前
        /// </summary>
        public Reactive<string> name = new Reactive<string>();

        /// <summary>
        /// MP
        /// </summary>
        public Reactive<LimitedValue<int>> mp = new Reactive<LimitedValue<int>>();
    }
}