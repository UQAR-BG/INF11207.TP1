namespace INF11207.TP1.Core;

public interface IObservable
{
    void Register(IObserver observer);
    void Remove(IObserver observer);
    void Notify();
}
