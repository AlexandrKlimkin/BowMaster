using System.Collections;
using System.Collections.Generic;
using Tools.BehaviourTree;
using UnityEngine;

public class MoveTask : UnitTask {
    public override TaskStatus Run() {
        var hasTarget = UnitBlackboard.Target != null;
        var direction = hasTarget ? 0 : Unit.TeamIndex == 0 ? 1f : -1f; //ToDo
        Unit.MoveController.Direction = direction;
        return TaskStatus.Success;
    }
}