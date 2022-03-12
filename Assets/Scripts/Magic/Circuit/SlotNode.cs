using System;
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

        this.spell.Drive(context);
        this.status = SlotNodeStatus.CoolingDown;
        this.cooledSeconds = 0;
    }

    private float cooledSeconds;
    private float coolTime = 0.2f;

    public void Update(DriveContext context)
    {
        if (this.status == SlotNodeStatus.CoolingDown)
        {
            cooledSeconds += context.deltaSeconds;
            if (cooledSeconds > coolTime)
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