using UnityEngine;
using System;

public class Util : MonoBehaviour
{
    private static readonly System.Random random = new System.Random();
    private static readonly object syncLock = new object();

    public static int RandomInt(int min, int max)
    {
        lock (syncLock)
        {
            return random.Next(min, max);
        }
    }

    public static double RandomDouble()
    {
        lock (syncLock)
        {
            return random.NextDouble();
        }
    }

    public static void InvokeIf(Action callback, bool condition)
    {
        if(condition) callback?.Invoke();
    }

}