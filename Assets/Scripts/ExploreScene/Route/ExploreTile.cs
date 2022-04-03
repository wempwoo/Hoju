using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreTile : Entity, ITouchable
{

    void Start()
    {
        //GetComponent<BoxCollider2D>().size = new Vector2(1000, 1000);
    }

    void Update()
    {

    }

    public void Touched(TouchEvent touch)
    {
        if (touch.state == TouchState.Began)
        {
            this.Destroy();
        }
    }
}
