using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExploreScene
{

    public class ExploreTile : Entity, ITouchable
    {
        Action touched;

        public void Initialize(ExploreRoom room, Action<ExploreRoom> touched)
        {
            this.touched = () => touched(room);
        }

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
                this.touched();
            }
        }
    }

}