using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using Object = System.Object;

public class GameObjectPoolContainer
{
    private readonly Dictionary<Type, MonoBehaviorPool> _Pools = new Dictionary<Type, MonoBehaviorPool>();

    public void RegisterPooledObject<T>(T obj) where T : MonoBehaviour
    {
        var pool = GetPool(typeof(T));
        if (pool == null)
        {
            pool = new MonoBehaviorPool(obj);
            _Pools.Add(typeof(T), pool);
        }
        else
        {
            Debug.LogWarning("Pool of this type already exists");
        }
    }

    private MonoBehaviorPool GetPool(Type type)
    {
        _Pools.TryGetValue(type, out MonoBehaviorPool value);
        return value;
    }

    public T Pop<T>(bool enableOnPop = true) where T : MonoBehaviour {
        var pool = GetPool(typeof(T));
        if (pool == null) {
            Debug.LogError($"pool not exists for type {typeof(T)}");
            return null;
        }
        var instance = pool.Instances.Count == 0 ? (T)CreatePoolObject(pool.Prefab) : (T)pool.Instances.Pop();
        if (enableOnPop)
            instance.gameObject.SetActive(true);
        return instance;
    }

    public void Push<T>(T poolObject) where T : MonoBehaviour {
        var pool = GetPool(typeof(T));
        if (pool == null) {
            Debug.LogError($"pool not exists for type {typeof(T)}");
            return;
        }
        pool.Instances.Push(poolObject);
        poolObject.gameObject.SetActive(false);
    }

    private static MonoBehaviour CreatePoolObject(MonoBehaviour prefab) {
        return UnityEngine.Object.Instantiate(prefab);
    }
}