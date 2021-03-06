﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable/*, ICameraTarget */{

    //public Animator Animator { get; private set; }
    public virtual float MaxHealth { get { return _MaxHealth; } protected set { _MaxHealth = value; } }
    [SerializeField]
    private float _MaxHealth;
    public virtual float Health {
        get {
            return _Health;
        }
        protected set {
            if (_Health != value) {
                _Health = value;
                OnHealthChanged?.Invoke();
            }
        }
    }
    public float RelativeHealth { get { return Health / MaxHealth; } }
    private float _Health;
    public Transform PointToFire { get; private set; }
    public virtual string MiddleDamageHitEffectName { get { return /*"BloodSparkEffect"*/ "FireSplashEffect"; } }

    public event Action OnHealthChanged;
    public event Action OnDamageTake;
    public event Action OnDeath;

    public bool Dead { get; private set; }

    public Vector3 LookPosition { get { return transform.position; } }

    protected virtual void Awake() {
        //Animator = GetComponentInChildren<Animator>();
        PointToFire = transform.Find("PointToFire");
        if (PointToFire == null) {
            PointToFire = new GameObject("PointToFire").transform;
            PointToFire.SetParent(transform);
            PointToFire.localPosition = Vector3.up / transform.localScale.y;
            PointToFire.localRotation = Quaternion.identity;
        }
    }

    protected virtual void Start() {
        Health = _MaxHealth;
    }

    public virtual void TakeDamage(Damage damage) {
        if (Dead)
            return;
        Health -= damage.Ammount;
        OnDamageTake?.Invoke();
        if (Health <= 0) {
            Health = 0;
            Die();
        }
        //if (Animator != null) {
        //    if (damage.Type == DamageType.Small) {
        //        OnSmallDamageTake();
        //    }
        //    else if (damage.Type == DamageType.Middle) {
        //        OnMiddleDamageTake();
        //    }
        //    else if (damage.Type == DamageType.Big) {
        //        OnBigDamageTake();
        //    }
        //}
    }

    public virtual void Die() {
        Dead = true;
        OnDeath?.Invoke();
        //if (Animator != null)
        //    Animator.SetBool("Dead", true);
    }

    protected virtual void OnSmallDamageTake() {

    }
    protected virtual void OnMiddleDamageTake() {
        if (!string.IsNullOrEmpty(MiddleDamageHitEffectName)) {
            //var effect = VisualEffect.GetEffect<ParticleEffect>(MiddleDamageHitEffectName);
            //effect.transform.position = PointToFire.position;
            //effect.transform.rotation = PointToFire.rotation;
            //effect.Parent = PointToFire;
            //effect.ResetLocalPosition();
            //effect.Play();
        }
        //if (!Dead) {
        //    Animator.SetTrigger("TakeMiddleDamage");
        //}
    }
    protected virtual void OnBigDamageTake() {
        //if (!Dead) {
        //    Animator.SetTrigger("TakeBigDamage");
        //}
    }

    protected virtual void OnDestroy() { }
}