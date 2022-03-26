using System;
using System.Collections.Generic;

/// <summary>
/// リアクティブなプロパティとか作るやつ
/// </summary>
/// <typeparam name="T"></typeparam>
public class Reactive<T>
{
    private T value;

    // TODO: 弱参照にすべきだと思う
    private readonly List<Action<T>> subscribers = new List<Action<T>>();

    public void Subscribe(Action<T> subscriber)
    {
        this.subscribers.Add(subscriber);
    }

    public T Value
    {
        get { return this.value; }
        set
        {
            this.value = value;
            this.subscribers.ForEach(s => s(this.value));
        }
    }

    /// <summary>
    /// 値を変更する（setとどちら使っても良い）
    /// </summary>
    /// <param name="changer"></param>
    public void Change(Func<T, T> changer)
    {
        this.Value = changer(this.Value);
    }
}
