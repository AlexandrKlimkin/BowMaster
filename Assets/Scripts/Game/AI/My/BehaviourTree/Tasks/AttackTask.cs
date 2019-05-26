using System.Collections;
using System.Collections.Generic;
using Tools.BehaviourTree;
using UnityEngine;

public class AttackTask : UnitTask
{
    public override TaskStatus Run() {
        if (Unit.AttackController.Weapon is MeleeWeapon) {
            var melee = (MeleeWeapon)Unit.AttackController.Weapon;
            melee.Target = UnitBlackboard.Target;
        }
        if (UnitBlackboard.Target) {
            Unit.AttackController.PerformHit();
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}