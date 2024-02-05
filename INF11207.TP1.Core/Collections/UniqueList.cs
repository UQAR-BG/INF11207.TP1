using System.Collections;

namespace INF11207.TP1.Core;

public class UniqueList<T>: IList<T>
{
    private IList<T> _items;

    public UniqueList(IList<T> values)
    {
        _items = MakeUnique(values);
    }
    
    public UniqueList(IEnumerable<T> values)
    {
        _items = MakeUnique(values);
    }
    
    T IList<T>.this[int index]
    {
        get => _items[index];
        set => AddIfNotPresent(value, index);
    }

    int ICollection<T>.Count => _items.Count;

    bool ICollection<T>.IsReadOnly => false;

    void ICollection<T>.Add(T item)
    {
        AddIfNotPresent(item);
    }

    void ICollection<T>.Clear()
    {
        _items.Clear();
    }

    bool ICollection<T>.Contains(T item)
    {
        return _items.Contains(item);
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    int IList<T>.IndexOf(T item)
    {
        return _items.IndexOf(item);
    }

    void IList<T>.Insert(int index, T item)
    {
        AddIfNotPresent(item, index);
    }

    bool ICollection<T>.Remove(T item)
    {
        return _items.Remove(item);
    }

    void IList<T>.RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    private IList<T> MakeUnique(IEnumerable<T> values)
    {
        return values.Distinct().ToList();
    }

    private void AddIfNotPresent(T item)
    {
        if (!_items.Contains(item))
            _items.Add(item);
    }
    
    private void AddIfNotPresent(T item, int index)
    {
        if (!item.Equals(_items[index]) && !_items.Contains(item))
            _items[index] = item;
    }
}