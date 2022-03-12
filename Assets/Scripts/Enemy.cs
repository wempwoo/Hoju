using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    void Start()
    {
        Velocity = new Vector2(0, -1);
    }

    void Update()
    {
        if (IsOutside)
        {
            Destroy();
        }
    }
}
