using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExploreScene
{
    public class ExploreDirector : MonoBehaviour
    {
        public Reactive<LimitedValue<int>> explorerMP = new Reactive<LimitedValue<int>>();

        void Start()
        {
            this.explorerMP.Value = new LimitedValue<int>(100, 100);
        }

        private Seconds delta = Seconds.zero;

        void Update()
        {
            this.delta += Seconds.Delta;

            if (this.delta > new Seconds(1))
            {
                this.delta = Seconds.zero;
                this.explorerMP.Change(mp => mp.NewCurrent(p => p - 1));
            }
        }
    }
}
