using INF11207.TP1.Core.Enums;

namespace INF11207.TP1.Core;

public abstract class TrackableObject : ITrackable
{
    protected virtual ObjectState State { get; set; } = ObjectState.New;

    public bool NeedsUpdating { get => IsNew() || IsDirty(); }

    public virtual bool IsClean()
    {
        return State == ObjectState.Clean;
    }

    public virtual bool IsDeleted()
    {
        return State == ObjectState.Deleted;
    }

    public virtual bool IsDirty()
    {
        return State == ObjectState.Dirty;
    }

    public virtual bool IsNew()
    {
        return State == ObjectState.New;
    }

    public virtual void RegisterClean()
    {
        State = ObjectState.Clean;
    }

    public virtual void RegisterDeleted()
    {
        State = ObjectState.Deleted;
    }

    public virtual void RegisterDirty()
    {
        State = ObjectState.Dirty;
    }

    public virtual void RegisterNew()
    {
        State = ObjectState.New;
    }
}
