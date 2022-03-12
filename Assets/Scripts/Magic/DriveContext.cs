using System;
using UnityEngine;

public class DriveContext
{
    public readonly Vector2 playerPosition;
    public readonly float deltaSeconds;

    public DriveContext(Vector2 playerPosition, float deltaSeconds)
    {
        this.playerPosition = playerPosition;
        this.deltaSeconds = deltaSeconds;
    }
}
