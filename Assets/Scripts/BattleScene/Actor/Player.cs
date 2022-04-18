using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BattleScene
{
    public class Player : Entity
    {
        private readonly List<Manaroid> manaroids = new List<Manaroid>();
        private readonly Prefab<Manaroid> manaroidPrefab = new Prefab<Manaroid>();

        void Start()
        {
            this.manaroids.Add(manaroidPrefab.Instantiate());
            this.manaroids.Add(manaroidPrefab.Instantiate());

            this.manaroids[0].Position = this.Position + new Vector2(-1, 1);
            this.manaroids[1].Position = this.Position + new Vector2(1, 1);

            this.manaroids[0].circuit = ManaCircuit.Sample1();
            this.manaroids[1].circuit = ManaCircuit.Sample2();
        }

        void Update()
        {

        }

    }
}