using System;
public struct Seconds
{
    public float seconds;

    public Seconds(float seconds)
    {
        this.seconds = seconds;
    }

    public static Seconds operator +(Seconds a, Seconds b)
    {
        return new Seconds(a.seconds + b.seconds);
    }

    public static Seconds operator -(Seconds a, Seconds b)
    {
        return new Seconds(a.seconds - b.seconds);
    }

    public static bool operator <(Seconds a, Seconds b)
    {
        return a.seconds < b.seconds;
    }

    public static bool operator >(Seconds a, Seconds b)
    {
        return a.seconds > b.seconds;
    }
}
