using System;
using UnityEngine;

public class DriveContext
{
    public readonly Vector2 playerPosition;
    public readonly Seconds deltaTime;

    public DriveContext(Vector2 playerPosition, Seconds deltaTime)
    {
        this.playerPosition = playerPosition;
        this.deltaTime = deltaTime;
    }
}
