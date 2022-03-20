using System;

namespace BattleScene
{
    public class MagicCircuit
    {
        private SlotNode slotsHead;

        public MagicCircuit(SlotNode slotsTree)
        {
            this.slotsHead = slotsTree;
        }

        private CircuitStatus status = CircuitStatus.Ready;

        private Seconds elapsed = new Seconds(0);

        private Seconds coolTime = new Seconds(1);

        public void Update(DriveContext context)
        {
            if (status == CircuitStatus.Ready)
            {
                status = CircuitStatus.Driving;
                slotsHead.Drive(context);
                return;
            }

            if (this.status == CircuitStatus.Driving)
            {
                this.slotsHead.Update(context);

                if (this.slotsHead.HasFinished())
                {
                    this.status = CircuitStatus.Cooldown;
                }
                return;
            }

            if (status == CircuitStatus.Cooldown)
            {
                this.elapsed += context.deltaTime;
                if (this.elapsed > this.coolTime)
                {
                    this.elapsed = new Seconds(0);
                    this.slotsHead.Reset();
                    this.status = CircuitStatus.Ready;
                }
                return;
            }
        }

        public static MagicCircuit Sample1()
        {

            var slot1 = new SlotNode();
            slot1.spell = new FireBallOrb();

            return new MagicCircuit(slot1);
        }

        public static MagicCircuit Sample2()
        {

            var slot1 = new SlotNode();
            slot1.spell = new ManaBoltOrb();

            var slot2 = new SlotNode();
            slot2.spell = new ManaBoltOrb();
            slot1.nextSlot = slot2;

            var slot3 = new SlotNode();
            slot3.spell = new ManaBoltOrb();
            slot2.nextSlot = slot3;

            return new MagicCircuit(slot1);
        }
    }

    public enum CircuitStatus
    {
        Ready,
        Driving,
        Cooldown,
    }
}