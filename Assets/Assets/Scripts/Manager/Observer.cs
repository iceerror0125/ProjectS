using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Message
{
    private object sender;
    private object data;
    public Message(object sender, object data) { }
}

public class Observer : SingletonMono<Observer>
{
    private Dictionary<EventType, List<Action>> observers = new Dictionary<EventType, List<Action>>();

    public void Subscribe(EventType type, Action action)
    {
        if (observers.ContainsKey(type))
        {
            observers[type].Add(action);
        }
        else
        {
            List<Action> list = new()
            {
                action
            };
            observers.Add(type, list);
        }
    }
    public void UnSubscribe(EventType type, Action action)
    {
        if (observers.ContainsKey(type))
        {
            observers[type].Remove(action);

            if (observers[type].Count == 0)
            {
                observers.Remove(type);
            }
        }
    }
    public void Annouce(EventType type = EventType.None)
    {
        if (observers.TryGetValue(type, out List<Action> list))
        {
            foreach (Action action in list)
            {
                action.Invoke();
            }
        }
    }
}
