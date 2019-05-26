using System;
using UnityEngine;

public static class ResourceLoader {
    //todo: resources caching
    public static T LoadResource<T>(string path, string name) where T : UnityEngine.Object {
        return LoadResource<T>(string.Concat(path, name));
    }

    public static T LoadResource<T>(ResourceAsset resourceAsset) where T : UnityEngine.Object {
        return LoadResource<T>(resourceAsset.Path, resourceAsset.Name);
    }

    public static T LoadResource<T>(string fullPath) where T : UnityEngine.Object {
        return Resources.Load<T>(fullPath);
    }

    public static T LoadResourceOnScene<T>(string fullPath, Transform parent = null, bool worldPositionStays = false) where T : UnityEngine.Object {
        var prefab = LoadResource<T>(fullPath);
        return UnityEngine.Object.Instantiate(prefab, parent, worldPositionStays);
    }

    public static T LoadResourceOnScene<T>(string fullPath, Vector3 position, Quaternion rotation, Transform parent = null) where T : UnityEngine.Object {
        var prefab = LoadResource<T>(fullPath);
        return UnityEngine.Object.Instantiate(prefab, position, rotation, parent);
    }

    public static void LoadResourceAsync<T>(string path, string name, Action<T> callback) where T : UnityEngine.Object {
        LoadResourceAsync(string.Concat(path, name), callback);
    }

    public static void LoadResourceAsync<T>(ResourceAsset resourceAsset, Action<T> callback) where T : UnityEngine.Object {
        LoadResourceAsync(resourceAsset.Path, resourceAsset.Name, callback);
    }

    public static void LoadResourceAsync<T>(string fullPath, Action<T> callback) where T : UnityEngine.Object {
        var request = Resources.LoadAsync<T>(fullPath);
        Action<AsyncOperation> completeAction = null;
        completeAction = _ => {
            request.completed -= completeAction;
            var result = request.asset as T;
            callback?.Invoke(result);
        };
        request.completed += completeAction;
    }

    public static void LoadResourceOnSceneAsync<T>(string fullPath, Action<T> callback, Transform parent = null, bool worldPositionStays = false) where T : UnityEngine.Object {
        LoadResourceAsync<T>(fullPath, _ => {
            var instance = UnityEngine.Object.Instantiate(_, parent, worldPositionStays);
            callback?.Invoke(instance);
        });
    }

    public static void LoadResourceOnSceneAsync<T>(string fullPath, Action<T> callback, Vector3 position, Quaternion rotation, Transform parent = null) where T : UnityEngine.Object {
        LoadResourceAsync<T>(fullPath, _ => {
            var instance = UnityEngine.Object.Instantiate(_, position, rotation, parent);
            callback?.Invoke(instance);
        });
    }
}