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

    public static ArcDegree Of(Vector2 v)
    {
        return new ArcDegree(Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
    }

    public static ArcDegree Top = new ArcDegree(90);

    /// <summary>
    /// 角度と長さからベクトルを求める
    /// </summary>
    /// <param name="arcDegree">角度(右が0度,上が90度)</param>
    /// <param name="distance">長さ</param>
    /// <returns></returns>
    public static Vector2 ToVector(ArcDegree arcDegree, float distance)
    {
        return new Vector2(
            Cos(arcDegree.value) * distance,
            Sin(arcDegree.value) * distance
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
}

