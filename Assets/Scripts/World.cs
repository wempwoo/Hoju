using System;
using UnityEngine;

public class World
{
    public static class Viewport
    {
        public static readonly Vector2 Min = Vector2.zero;
        public static readonly Vector2 Max = Vector2.one;
    }

    public static class Point
    {
        public static Vector2 Min
        {
            get { return FromViewport(Viewport.Min); }
        }

        public static Vector2 Max
        {
            get { return FromViewport(Viewport.Max); }
        }

        public static Vector2 FromViewport(Vector2 v)
        {
            return Camera.main.ViewportToWorldPoint(v);
        }
    }
}
