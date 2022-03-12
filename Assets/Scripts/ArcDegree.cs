using System;
using UnityEngine;

public class ArcDegree
{
    /// <summary>
    /// 角度と長さからベクトルを求める
    /// </summary>
    /// <param name="arcDegree">角度(右が0度,上が90度)</param>
    /// <param name="distance">長さ</param>
    /// <returns></returns>
    public static Vector2 ToVector(float arcDegree, float distance)
    {
        return new Vector2(
            Cos(arcDegree) * distance,
            Sin(arcDegree) * distance
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

public static class Vector2Extension
{
    /// <summary>
    /// ベクトルの角度を返す
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static float ArcDegree(this Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
