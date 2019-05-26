using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

    public override void OnStartAttack() {
        //Controller.MoveController.StanOnTime(2.2f);
    }

    public Actor Target;

    public override void Hit() {
        base.Hit();
        if (Target == null)
            return;
        var damage = new Damage(Damage, Controller.Owner);
        Target.TakeDamage(damage);
    }
}
