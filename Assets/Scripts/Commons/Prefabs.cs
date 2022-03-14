using System;
using UnityEngine;
public static class Prefabs
{
    public static GameObject Load(string name)
    {
        return Resources.Load("Prefabs/" + name) as GameObject;
    }

    public static T Instantiate<T>(GameObject prefab)
    {
        var instance = MonoBehaviour.Instantiate(prefab);
        return instance.GetComponent<T>();
    }
}
