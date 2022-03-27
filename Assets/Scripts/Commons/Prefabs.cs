using System;
using UnityEngine;
public static class Prefabs
{
    public static GameObject Load(string name)
    {
        string path = "Prefabs/" + name;
        var prefab = Resources.Load(path) as GameObject;

        if (prefab == null)
        {
            throw new Exception("not found: " + path);
        }

        return prefab;
    }

    public static T Instantiate<T>(string name)
    {
        return Instantiate<T>(Load(name));
    }

    public static GameObject Instantiate(string name)
    {
        return GameObject.Instantiate(Load(name));
    }

    public static T Instantiate<T>(GameObject prefab)
    {
        var instance = MonoBehaviour.Instantiate(prefab);
        return instance.GetComponent<T>();
    }
}
