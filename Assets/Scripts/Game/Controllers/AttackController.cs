using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    public Unit Owner { get; private set; }
    public Weapon Weapon { get; private set; }

    public bool CanAttack { get { return !Owner.Dead; } }

    protected virtual void Awake() {
        Owner = GetComponentInParent<Unit>();
        Weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void PerformHit() {
        if(Weapon.Reloaded)
            Weapon.Hit();
    }

    public bool CheckActorInWeaponRange(Actor other, out float sqrDist) {
        sqrDist = SqrDistanceToActor(other);
        if (Weapon == null)
            return false;
        if (sqrDist < Weapon.SqrRange)
            return true;
        else
            return false;
    }

    public float SqrDistanceToActor(Actor other) {
        if (other == null)
            return float.PositiveInfinity;
        return Vector3.SqrMagnitude(Owner.transform.position - other.transform.position);
    }
}
