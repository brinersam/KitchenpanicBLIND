using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

[Serializable] // todo make custom editor so i can see whats going on in here
public class LimitedSizeStack<T> : IEnumerable<T>
{
    readonly T[] data;
    readonly int maxSize;
    private int curSize;
    public bool IsFull => curSize == maxSize;// data.All(x => x != null);
    public bool IsEmpty => !data.Any(x => x != null);
    public int Count => curSize;//data.Count(x => x != null);
    public int MaxSize => maxSize;
    
    public LimitedSizeStack(int size)
    {
        data = new T[size];
        maxSize = size;
        curSize = 0;
    }

    public bool TryPush(T item)
    {
        if (curSize < maxSize)
        {
            data[curSize++] = item;
            return true;
        }
        return false;
    }

    public T Peek()
    {
        if (curSize - 1 < 0) return default;

        return data[curSize-1];
    }
    public T Pop()
    {
        if (curSize - 1 < 0) return default;

        T temp = data[--curSize];
        data[curSize] = default;
        return temp;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int idx = 0; idx < curSize; idx++)
        {
            if (data[idx] == null) yield break;
            
            yield return data[idx];
        }
            
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}