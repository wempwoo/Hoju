using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BattleScene
{
    public class Player : Entity
    {
        private Manaroid manaroid;

        void Start()
        {
            this.manaroid = Prefabs.Instantiate<Manaroid>("ManaroidPrefab");
            this.manaroid.Position = this.Position + new Vector2(0, 1);
        }

        void Update()
        {

        }

    }
}