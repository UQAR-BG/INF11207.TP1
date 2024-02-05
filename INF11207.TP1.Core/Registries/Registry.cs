namespace INF11207.TP1.Core;

public static class Registry
{
    private static Dictionary<string, IIdentifiable> _register = new();
    private static object _lockKey = new();
    
    public static T CreateAndRegister<T>(string id) where T : IIdentifiable, new()
    {
        T retour = new T();
        lock (_lockKey)
        {
            bool alreadyRegistered = _register.TryGetValue(id, out var result);

            if (!alreadyRegistered)
            {
                result = retour;
                _register.Add(id, result);
            }
        }

        return retour;
    }
}