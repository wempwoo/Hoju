using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 魔導機
    /// </summary>
    public class Manaroid : Entity
    {
        public MagicCircuit circuit;

        void Start()
        {
            this.circuit = new MagicCircuit(this.CreateSlotsTree());
        }

        private SlotNode CreateSlotsTree()
        {
            var slot1 = new SlotNode();
            slot1.spell = new SpellOrb(new ManaBolt());

            var slot2 = new SlotNode();
            slot2.spell = new SpellOrb(new ManaBolt());
            slot1.nextSlot = slot2;

            var slot3 = new SlotNode();
            slot3.spell = new SpellOrb(new ManaBolt());
            slot2.nextSlot = slot3;

            return slot1;
        }

        void Update()
        {
            Seconds deltaTime = new Seconds(Time.deltaTime);
            var driveContext = new DriveContext(this.Position, deltaTime);
            circuit.Update(driveContext);
        }
    }
}
