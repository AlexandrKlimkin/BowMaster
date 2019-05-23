using UnityEngine;

public class Damage {

    public Damage() { }

    public Damage(float ammount, Actor instigator = null) {
        Ammount = ammount;
        Instigator = instigator;
    }

    public Actor Instigator;
    public float Ammount;
    public Vector3 Position;
    public Vector3 Normal;
    public EffectType EffectType;
    public int WeaponTypeData;
}

public enum EffectType {
    None,
    Big,
    Small,
}