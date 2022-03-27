using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// リアクティブなリストのプロパティとか作るやつ
/// </summary>
/// <typeparam name="T"></typeparam>
public class ReactiveList<T> : IList<T>
{
    private readonly List<T> list = new List<T>();

    // TODO: 弱参照にすべきだと思う
    private readonly List<Action<List<T>, ReactiveListChange, T>> subscribers = new List<Action<List<T>, ReactiveListChange, T>>();

    public void Subscribe(Action<List<T>, ReactiveListChange, T> subscriber)
    {
        this.subscribers.Add(subscriber);

        subscriber(this.list, ReactiveListChange.Init, default(T));
    }

    private void Notify(ReactiveListChange change, T item)
    {
        this.subscribers.ForEach(s => s(this.list, change, item));
    }

    public T this[int index] { get => this.list[index]; set => throw new NotSupportedException(); }

    public int Count => this.list.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        this.list.Add(item);
        this.Notify(ReactiveListChange.Add, item);
    }

    public void Clear()
    {
        var items = this.list.ToList();
        this.list.Clear();

        items.ForEach(item => this.Notify(ReactiveListChange.Remove, item));
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
        this.Notify(ReactiveListChange.Add, item);
    }

    public bool Remove(T item)
    {
        bool result = this.Remove(item);
        this.Notify(ReactiveListChange.Remove, item);
        return result;
    }

    public void RemoveAt(int index)
    {
        var removed = this[index];
        this.RemoveAt(index);
        this.Notify(ReactiveListChange.Remove, removed);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

public enum ReactiveListChange
{
    Init,
    Add,
    Remove,
}
