using System;

namespace BattleScene
{
    public class SlotNode
    {
        public SpellOrb spell;

        public SlotNode nextSlot;

        private SlotNodeStatus status = SlotNodeStatus.Ready;

        public void Drive(DriveContext context)
        {
            if (this.status != SlotNodeStatus.Ready)
            {
                throw new Exception("invalid status: " + this.status);
            }

            this.spell?.Drive(context);
            this.status = SlotNodeStatus.CoolingDown;
            this.cooled = new Seconds(0);
        }

        private Seconds cooled;
        private Seconds coolTime = new Seconds(0.2f);

        public void Update(DriveContext context)
        {
            if (this.status == SlotNodeStatus.CoolingDown)
            {
                cooled += context.deltaTime;
                if (cooled > coolTime)
                {
                    this.status = SlotNodeStatus.Done;
                    this.nextSlot?.Drive(context);
                }

                return;
            }

            this.nextSlot?.Update(context);
        }

        public bool HasFinished()
        {
            return this.status == SlotNodeStatus.Done
                && (this.nextSlot?.HasFinished() ?? true);
        }

        public void Reset()
        {
            this.status = SlotNodeStatus.Ready;
            this.nextSlot?.Reset();
        }
    }

    public enum SlotNodeStatus
    {
        Ready,
        CoolingDown,
        Done,
    }
}