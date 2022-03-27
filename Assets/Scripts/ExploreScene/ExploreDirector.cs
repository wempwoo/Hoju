using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExploreScene
{
    public class ExploreDirector : MonoBehaviour
    {
        public readonly PlayerState player = new PlayerState();

        void Start()
        {
        }

        private Seconds delta = Seconds.zero;

        void Update()
        {
            this.delta += Seconds.Delta;

            if (this.delta > new Seconds(1))
            {
                this.delta = Seconds.zero;
                this.player.explorer.mp.Change(mp => mp.NewCurrent(p => p - 1));
            }
        }
    }
}
