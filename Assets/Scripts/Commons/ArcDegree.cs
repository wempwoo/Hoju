using System;
using UnityEngine;

/// <summary>
/// 角度(360度のやつ)
/// </summary>
public struct ArcDegree
{
    public float value;

    public ArcDegree(float arcDegree)
    {
        this.value = arcDegree;
    }

    public static ArcDegree operator +(ArcDegree a, ArcDegree b)
    {
        return new ArcDegree(a.value + b.value);
    }

    public static ArcDegree operator -(ArcDegree a, ArcDegree b)
    {
        return new ArcDegree(a.value - b.value);
    }

    public static ArcDegree Of(Vector2 v)
    {
        return new ArcDegree(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }

    public static ArcDegree Top = new ArcDegree(90);

    /// <summary>
    /// 角度と長さからベクトルを求める
    /// </summary>
    /// <param name="distance">長さ</param>
    /// <returns></returns>
    public Vector2 ToVector(float distance)
    {
        return new Vector2(
            Cos(this.value) * distance,
            Sin(this.value) * distance
        );
    }

    /// Mathf.Cosの角度指定版.
    public static float Cos(float Deg)
    {
        return Mathf.Cos(Mathf.Deg2Rad * Deg);
    }

    /// Mathf.Sinの角度指定版.
    public static float Sin(float Deg)
    {
        return Mathf.Sin(Mathf.Deg2Rad * Deg);
    }

    /// <summary>
    /// vectorを回転させる
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Vector2 Rotate(Vector2 vector, ArcDegree rotation)
    {
        var distance = Vector2.Distance(Vector2.zero, vector);
        var currentDirection = Of(vector);
        var rotated = currentDirection + rotation;

        return rotated.ToVector(distance);
    }
}

