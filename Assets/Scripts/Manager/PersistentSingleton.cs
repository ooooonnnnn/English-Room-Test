using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[Tooltip("A singleton that will be destroyed if an older one with the same name exists")]
public class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
{
    private static Dictionary<string, PersistentSingleton<T>> singletonInstances = new();
    public static T Instance => _thisInstance;
    protected static T _thisInstance;

    protected virtual void Awake()
    {
        // print($"Awake called on PersistentSingleton on {gameObject.name}");
        
        var gameObjectName = gameObject.name;
        
        if (!singletonInstances.ContainsKey(gameObjectName))
        {
            singletonInstances.Add(gameObjectName, this);
            DontDestroyOnLoad(gameObject);
            _thisInstance = (T)this;
            return;
        }
        
        if (singletonInstances[gameObjectName] != this) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (singletonInstances[gameObject.name] == this) singletonInstances.Remove(gameObject.name);
    }
}
