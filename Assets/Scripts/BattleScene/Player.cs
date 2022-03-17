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
            this.manaroid.circuit = new MagicCircuit(this.CreateSlotsTree());
        }

        private SlotNode CreateSlotsTree()
        {
            var slot1 = new SlotNode();
            slot1.spell = new SpellOrb();

            var slot2 = new SlotNode();
            slot2.spell = new SpellOrb();
            slot1.nextSlot = slot2;

            var slot3 = new SlotNode();
            slot3.spell = new SpellOrb();
            slot2.nextSlot = slot3;

            return slot1;
        }

        void Update()
        {

        }

    }
}