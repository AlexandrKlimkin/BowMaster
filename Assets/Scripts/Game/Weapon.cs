using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public AttackController Controller { get; private set; }

    public float Damage;
    public float Range;
    public float ReloadTime;
    private float _CoolDownTime;


    public float SqrRange { get { return Range * Range; } }
    public virtual bool Reloaded {
        get {
            return Time.time >= _CoolDownTime;
        }
    }

    protected virtual void Awake() {
        Controller = GetComponentInParent<AttackController>();
    }

    public abstract void OnStartAttack();

    public virtual void Hit() {
        _CoolDownTime = Time.time + ReloadTime;
        OnStartAttack();
    }
}
