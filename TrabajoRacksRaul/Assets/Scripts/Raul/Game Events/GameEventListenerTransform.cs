using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerTransform : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventTransform Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<Transform> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Transform t)
    {

        Response.Invoke(t);
    }
}
