using System;
using UnityEngine;

public struct Seconds
{
    public static readonly Seconds zero = new Seconds(0);

    public static Seconds Delta
    {
        get { return new Seconds(Time.deltaTime); }
    }

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
