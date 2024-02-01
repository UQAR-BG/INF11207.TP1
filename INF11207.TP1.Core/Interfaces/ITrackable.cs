namespace INF11207.TP1.Core;

internal interface ITrackable
{
    bool NeedsUpdating { get; }
    bool IsClean();
    bool IsDeleted();
    bool IsDirty();
    bool IsNew();
    void RegisterClean();
    void RegisterDeleted();
    void RegisterDirty();
    void RegisterNew();
}
