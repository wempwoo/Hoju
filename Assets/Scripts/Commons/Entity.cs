using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D = null;
    public Rigidbody2D Rigidbody => _rigidbody2D ??= GetComponent<Rigidbody2D>();

    SpriteRenderer _renderer = null;
    public SpriteRenderer Renderer => _renderer ??= GetComponent<SpriteRenderer>();


    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }


    public Vector2 Velocity
    {
        get => Rigidbody.velocity;
        set => Rigidbody.velocity = value;
    }

    public Vector2 Size
    {
        get => Renderer.bounds.size;
    }

    public Vector2 WorldMin
    {
        get => World.Point.Min - Size;
    }

    public Vector2 WorldMax
    {
        get => World.Point.Max + Size;
    }

    /// 画面外に出たかどうか.
    public bool IsOutside
    {
        get
        {
            Vector2 min = WorldMin;
            Vector2 max = WorldMax;
            Vector2 pos = Position;
            return pos.x < min.x || pos.y < min.y || pos.x > max.x || pos.y > max.y;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// targetsの中でfromに最も近いEntityを返す
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <param name="targets">必ず1つ以上</param>
    /// <param name="from"></param>
    /// <returns></returns>
    public static (E entity, float distance) GetNearest<E>(
        IEnumerable<E> targets,
        Vector2 from)
        where E : Entity
    {
        if (targets.Count() == 0)
        {
            throw new ArgumentException("targetsは1つ以上の要素が必要");
        }

        var nearest = targets
                .Select(t => new
                {
                    ent = t,
                    dist = Vector2.Distance(from, t.transform.position)
                })
                .OrderBy(t => t.dist)
                .FirstOrDefault();

        return (nearest.ent, nearest.dist);
    }
}
