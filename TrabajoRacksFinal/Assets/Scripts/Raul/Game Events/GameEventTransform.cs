using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventTransform : ScriptableObject
{
    private readonly List<GameEventListenerTransform> eventListeners =
        new List<GameEventListenerTransform>();

    public void Raise(Transform t)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(t);
    }

    public void RegisterListener(GameEventListenerTransform listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerTransform listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
