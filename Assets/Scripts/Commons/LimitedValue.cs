using System;

/// <summary>
/// 上限を持つ値（HP/MAXHPとか）
/// </summary>
public class LimitedValue<T>
{
    private readonly T current;
    private readonly T max;

    public T Current { get { return this.current; } }
    public T Max { get { return this.max; } }

    public LimitedValue(T current, T max)
    {
        this.current = current;
        this.max = max;
    }

    public LimitedValue<T> NewCurrent(Func<T, T> changer)
    {
        return new LimitedValue<T>(changer(this.current), this.max);
    }

    public LimitedValue<T> NewMax(Func<T, T> changer)
    {
        return new LimitedValue<T>(this.current, changer(this.max));
    }

    public override string ToString()
    {
        return $"{current}/{max}";
    }
}
