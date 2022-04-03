using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タッチ（クリック）を検出する
/// </summary>
public class TouchDetector : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var touch = TouchEvent.Create();
        if (touch == null) return;

        var worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
        var hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit)
        {
            hit.collider.GetComponent<ITouchable>()?.Touched(touch);
        }
    }

}
