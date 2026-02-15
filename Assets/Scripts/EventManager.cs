using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<Vector3, Quaternion> OnCrystalPicked;
    public static event Action OnRopeTriggered;
    public static event Action OnRopeExit;
    public static void InvokeCrystalPicked(Vector3 position, Quaternion rotation)
    {
        OnCrystalPicked.Invoke(position, rotation);
    }
    public static void InvokeRopeTriggered()
    {
        OnRopeTriggered.Invoke();
    }
    public static void InvokeRopeExit()
    {
        OnRopeExit.Invoke();
    }
}
