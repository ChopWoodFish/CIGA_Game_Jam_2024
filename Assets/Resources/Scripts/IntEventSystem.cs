using System;
using System.Collections.Generic;
using UnityEngine;

public static class IntEventSystem
{
    private static Dictionary<int, Action<object>> dictEventListener = new Dictionary<int, Action<object>>();

    public static void Send(GameEventEnum eventEnum, object param)
    {
        int eventID = (int) eventEnum;
        if (dictEventListener.TryGetValue(eventID, out var listener))
        {
            try
            {
                listener?.Invoke(param);
            }
            catch (Exception e)
            {
                Debug.LogError($"IntEventSystem: {e}");
            }
        }
    }

    public static void Register(GameEventEnum eventEnum, Action<object> listener)
    {
        int id = (int) eventEnum;
        Register(id, listener);
    }

    public static void Register(int eventID, Action<object> listener)
    {
        if (dictEventListener.ContainsKey(eventID))
        {
            dictEventListener[eventID] += listener;
        }
        else
        {
            dictEventListener.Add(eventID, listener);
        }
    }

    public static void Unregister(GameEventEnum eventEnum, Action<object> listener)
    {
        int id = (int) eventEnum;
        Unregister(id, listener);
    }

    public static void Unregister(int eventID, Action<object> listener)
    {
        if (dictEventListener.ContainsKey(eventID))
        {
            dictEventListener[eventID] -= listener;
            if (dictEventListener[eventID] == null)
            {
                dictEventListener.Remove(eventID);
            }
        }
    }
}