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

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (Instance && (Instance != this))
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
            return;
        }
        Instance = (T)this;
    }
#endif

#if UNITY_EDITOR
    public static T LoadSingletonAsset()
    {
        var assetGUIDS = AssetDatabase.FindAssets(typeof(T).ToString());
        if (assetGUIDS.Length == 0)
        {
            Debug.LogWarning($"No singleton assets of type {typeof(T)} found");
            return null;
        }
        var foundAssetPath = AssetDatabase.GUIDToAssetPath(assetGUIDS[0]);
        return AssetDatabase.LoadAssetAtPath<T>(foundAssetPath);
    }
#endif
}
