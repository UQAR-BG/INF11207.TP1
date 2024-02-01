using INF11207.TP1.Core.Enums;

namespace INF11207.TP1.Core;

public class ObservableList<T> : TrackableList<T>, IObservable
{
    private readonly List<IObserver> observers;

    private ObjectState _state;
    protected override ObjectState State 
    {
        get => _state;
        set
        {
            _state = value;
            if (value == ObjectState.New || value == ObjectState.Dirty)
                Notify();
        }
    }

    public ObservableList(IObserver observer) : this(observer, new List<T>()) { }

    public ObservableList(IObserver observer, IList<T> values) : base()
    {
        trackables = new List<T>(values);

        observers = new List<IObserver>();
        Register(observer);
    }

    public void Notify()
    {
        observers.ForEach(o => o.Notify());
    }

    public void Register(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Remove(IObserver observer)
    {
        observers.Remove(observer);
    }
}
