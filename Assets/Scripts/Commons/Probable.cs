using System;
using System.Collections.Generic;

/// <summary>
/// 確率による分岐を記述する
/// </summary>
public class Probable
{
    public static void Single(float probability, Action action)
    {
        if (UnityEngine.Random.value < probability)
        {
            action();
        }
    }

    /// <summary>
    /// 複数のCaseそれぞれに確率判定がされ、高々一つだけ実行される
    /// </summary>
    /// <returns></returns>
    public static AtMostOneProbable AtMostOne()
    {
        return AtMostOneProbable.Start();
    }


    /// <summary>
    /// 複数のCaseそれぞれに確率判定がされ、高々一つだけ実行される
    /// </summary>
    public class AtMostOneProbable
    {
        public readonly bool alive;

        private AtMostOneProbable(bool alive)
        {
            this.alive = alive;
        }

        private static readonly AtMostOneProbable Alive = new AtMostOneProbable(true);
        private static readonly AtMostOneProbable Dead = new AtMostOneProbable(false);


        public static AtMostOneProbable Start()
        {
            return Alive;
        }

        public AtMostOneProbable Case(float probability, Action action)
        {
            if (!this.alive) return Dead;

            if (UnityEngine.Random.value < probability)
            {
                action();
                return Dead;
            }

            return Alive;
        }

        public void Else(Action action)
        {
            if (this.alive) action();
        }
    }

    /// <summary>
    /// elementsの要素すべてに対して順次、等確率で実行判断していく
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="probability"></param>
    /// <param name="elements"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static SequenceProbable Sequence<T>(float probability, IEnumerable<T> elements, Action<T> action)
    {
        foreach (var e in elements)
        {
            if (UnityEngine.Random.value < probability)
            {
                action(e);
                return SequenceProbable.Dead;
            }
        }

        return SequenceProbable.Alive;
    }

    public class SequenceProbable
    {
        private readonly bool alive;
        private SequenceProbable(bool alive) { this.alive = alive; }

        public static readonly SequenceProbable Alive = new SequenceProbable(true);
        public static readonly SequenceProbable Dead = new SequenceProbable(false);

        public void Else(Action action)
        {
            if (this.alive) action();
        }
    }
}
