using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used instead of List of List of T to be able to serialize it
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class SerializableList<T> : IEnumerable<T> where T : UnityEngine.Object
{
    public List<T> list = new();

    public void Add(T item) => list.Add(item);
    public T Find(Predicate<T> predicate) => list.Find(predicate);
    public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
