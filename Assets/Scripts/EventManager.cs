using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<Vector3, Quaternion> OnCrystalPicked;
    public static void InvokeCrystalPicked(Vector3 position, Quaternion rotation)
    {
        OnCrystalPicked.Invoke(position, rotation);
    }
}
