using System;
using System.Collections.Generic;

namespace ExploreScene
{
    public class PlayerState
    {
        public readonly ExplorerState explorer = new ExplorerState();

        public readonly ReactiveList<ManaroidState> manaroids = new ReactiveList<ManaroidState>();

        public PlayerState()
        {
            this.explorer.name.Value = "たんさくしゃX";
            this.explorer.mp.Value = new LimitedValue<int>(40, 100);

            var manaroid1 = new ManaroidState();
            manaroid1.name.Value = "XG-100";
            manaroid1.hp.Value = new LimitedValue<int>(485, 600);
            manaroid1.manaCharge.Value = new LimitedValue<int>(5, 25);

            var manaroid2 = new ManaroidState();
            manaroid2.name.Value = "KDL-2A89";
            manaroid2.hp.Value = new LimitedValue<int>(304, 380);
            manaroid2.manaCharge.Value = new LimitedValue<int>(22, 50);

            this.manaroids.Add(manaroid1);
            this.manaroids.Add(manaroid2);
        }
    }
}