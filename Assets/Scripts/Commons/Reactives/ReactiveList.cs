using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// リアクティブなリストのプロパティとか作るやつ
/// </summary>
/// <typeparam name="T"></typeparam>
public class ReactiveList<T> : IList<T>
{
    private readonly List<T> list = new List<T>();

    // TODO: 弱参照にすべきだと思う
    private readonly List<Action<List<T>>> subscribers = new List<Action<List<T>>>();

    public void Subscribe(Action<List<T>> subscriber)
    {
        this.subscribers.Add(subscriber);
    }

    private void Notify()
    {
        this.subscribers.ForEach(s => s(this.list));
    }

    public T this[int index] { get => this.list[index]; set { this.list[index] = value; this.Notify(); } }

    public int Count => this.list.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        this.list.Add(item);
        this.Notify();
    }

    public void Clear()
    {
        this.list.Clear();
        this.Notify();
    }

    public bool Contains(T item)
    {
        return this.list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        this.list.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return this.list.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return this.list.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        this.list.Insert(index, item);
        this.Notify();
    }

    public bool Remove(T item)
    {
        bool result = this.Remove(item);
        this.Notify();
        return result;
    }

    public void RemoveAt(int index)
    {
        this.RemoveAt(index);
        this.Notify();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
