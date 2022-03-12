using System;
public struct Lifespan
{
    public float value;

    public Lifespan(float value)
    {
        this.value = value;
    }

    public float AsSeconds
    {
        get => value;
    }
}
