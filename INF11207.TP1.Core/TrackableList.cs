using System.Collections;

namespace INF11207.TP1.Core;

public class TrackableList<T> : TrackableObject, IList<T>
{
    protected List<T> trackables;

    public TrackableList()
    {
        trackables = new List<T>();
    }

    T IList<T>.this[int index] 
    { 
        get => trackables[index];
        set 
        { 
            trackables[index] = value; 
            RegisterDirty();
        }
    }

    int ICollection<T>.Count => trackables.Count;

    bool ICollection<T>.IsReadOnly => false;

    void ICollection<T>.Add(T item)
    {
        trackables.Add(item);
        RegisterDirty();
    }

    void ICollection<T>.Clear()
    {
        trackables.Clear();
        RegisterClean();
    }

    bool ICollection<T>.Contains(T item)
    {
        return trackables.Contains(item);
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
        trackables.CopyTo(array, arrayIndex);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return trackables.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return trackables.GetEnumerator();
    }

    int IList<T>.IndexOf(T item)
    {
        return trackables.IndexOf(item);
    }

    void IList<T>.Insert(int index, T item)
    {
        trackables.Insert(index, item);
        RegisterDirty();
    }

    bool ICollection<T>.Remove(T item)
    {
        bool result = trackables.Remove(item);
        
        if (result)
            RegisterDirty();

        return result;  
    }

    void IList<T>.RemoveAt(int index)
    {
        trackables.RemoveAt(index);
        RegisterDirty();
    }
}
