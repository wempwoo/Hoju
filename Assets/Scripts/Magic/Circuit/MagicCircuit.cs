using System;
public class MagicCircuit
{
    private SlotNode slotsHead;

    public MagicCircuit(SlotNode slotsTree)
    {
        this.slotsHead = slotsTree;
    }

    private CircuitStatus status = CircuitStatus.Ready;

    private float elapsedSeconds = 0;

    private float coolTimeSeconds = 1;

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
            this.elapsedSeconds += context.deltaSeconds;
            if (this.elapsedSeconds > this.coolTimeSeconds)
            {
                this.elapsedSeconds = 0;
                this.slotsHead.Reset();
                this.status = CircuitStatus.Ready;
            }
            return;
        }
    }
}

public enum CircuitStatus
{
    Ready,
    Driving,
    Cooldown,
}