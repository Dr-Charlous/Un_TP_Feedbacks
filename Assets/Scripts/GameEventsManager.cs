using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    static GameEventsManager s_instance;

    public List<GameEvent> GameEvents = new List<GameEvent>();

    Dictionary<string, GameEvent> _events;

    private void Awake()
    {
        s_instance = this;

        _events = new Dictionary<string, GameEvent>(GameEvents.Count);

        foreach (var item in GameEvents)
        {
            _events.Add(item.name, item);
        }
    }

    public static void PlayEvent(string eventName, GameObject go)
    {
        if (s_instance._events[eventName].GameObject == null)
            s_instance._events[eventName].GameObject = go;
        s_instance.StartCoroutine(s_instance._events[eventName].Execute());
    }

    private void Start()
    {
        foreach (var item in GameEvents)
            PlayEvent(item.name, this.gameObject);
    }
}
