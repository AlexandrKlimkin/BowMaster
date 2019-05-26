using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Arrow
{
    public Projectile Projectile;
    public float Cooldown = 1f;
    public float Damage;
    public bool Recharged => Time.time - LastShotTime >= Cooldown;
    public float LastShotTime { get; set; } = float.NegativeInfinity;
    public float NormalizedCD => Recharged ? 1f : (Time.time - LastShotTime) / Cooldown;
}
