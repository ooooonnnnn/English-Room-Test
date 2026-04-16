using System;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Base class for all singleton scriptable objects
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonSO<T> : ScriptableObject where T : SingletonSO<T>
{
    public static T Instance { get; private set; }

    protected virtual void OnValidate()
    {
        if (Instance && (Instance != this))
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
            return;
        }
        Instance = (T)this;
    }
}
