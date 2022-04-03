using System;
using UnityEngine;

public class TouchEvent
{
    public readonly TouchState state;

    public readonly Vector2 position;

    public TouchEvent(TouchState state, Vector2 position)
    {
        this.state = state;
        this.position = position;
    }

    public static TouchEvent Create()
    {
        // エディタ
        if (Application.isEditor)
        {
            TouchState state;
            if (Input.GetMouseButtonDown(0))
            {
                state = TouchState.Began;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                state = TouchState.Ended;
            }
            else if (Input.GetMouseButton(0))
            {
                state = TouchState.Touching;
            }
            else
            {
                return null;
            }

            return new TouchEvent(state, Input.mousePosition);
        }

        // スマホ
        else
        {
            if (Input.touchCount == 0) return null;

            Touch touch = Input.GetTouch(0);

            var state = touch.phase switch
            {
                TouchPhase.Began => TouchState.Began,
                TouchPhase.Moved => TouchState.Touching,
                TouchPhase.Stationary => TouchState.Touching,
                TouchPhase.Ended => TouchState.Ended,
                TouchPhase.Canceled => TouchState.Ended,
                _ => throw new System.NotImplementedException(),
            };

            return new TouchEvent(state, touch.position);
        }

    }
}
