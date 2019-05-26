using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviorPool {
    public readonly Stack<MonoBehaviour> Instances = new Stack<MonoBehaviour>();
    public readonly MonoBehaviour Prefab;

    public Transform Host { get; set; }

    public MonoBehaviorPool(MonoBehaviour prefab) {
        Prefab = prefab;
    }
}