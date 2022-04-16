using System;
using UnityEngine;

public class Prefab
{
    private Lazy<GameObject> prefab;

    public Prefab(string name)
    {
        this.prefab = new Lazy<GameObject>(() => LoadPrefab(name));
    }

    public T Instantiate<T>()
    {
        return Instantiate().GetComponent<T>();
    }

    public GameObject Instantiate()
    {
        return GameObject.Instantiate(prefab.Value);
    }

    private static GameObject LoadPrefab(string name)
    {
        string path = "Prefabs/" + name;
        var prefab = Resources.Load(path) as GameObject;

        if (prefab == null)
        {
            throw new Exception("not found: " + path);
        }

        return prefab;
    }

}
