using System;
using UnityEngine;

public class Prefab<T>
{
    private Lazy<GameObject> prefab;

    public Prefab()
    {
        string name = typeof(T).FullName.Replace('.', '/') + "Prefab";
        this.prefab = new Lazy<GameObject>(() => LoadPrefab(name));
    }

    public T Instantiate()
    {
        return GameObject.Instantiate(prefab.Value).GetComponent<T>();
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
