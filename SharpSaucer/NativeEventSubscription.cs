using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpSaucer;

/// <summary>
/// Tracks native event subscriptions for a single saucer event type,
/// mapping managed delegates to their native subscription IDs.
/// </summary>
internal sealed class NativeEventSubscription<TManaged> where TManaged : Delegate
{
    private readonly Dictionary<TManaged, (nuint Id, Delegate Native)> _subscriptions = [];

    public int Count => _subscriptions.Count;

    public void Add(TManaged handler, nuint id, Delegate nativeDelegate)
        => _subscriptions[handler] = (id, nativeDelegate);

    public bool TryRemove(TManaged handler, out nuint id)
    {
        if (_subscriptions.Remove(handler, out var entry))
        {
            id = entry.Id;
            return true;
        }

        id = 0;
        return false;
    }

    public void Clear() => _subscriptions.Clear();
}
