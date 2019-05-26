using Tools.BehaviourTree;

public class FindTargetTask : UnitTask {
    public override TaskStatus Run() {
        foreach (var unit in Unit.ActiveUnits) {
            if (unit == Unit)
                continue;
            if(unit.TeamIndex == Unit.TeamIndex)
                continue;
            var minDist = float.PositiveInfinity;
            if (Unit.AttackController.CheckActorInWeaponRange(unit, out float dist)) {
                if (dist < minDist) {
                    minDist = dist;
                    UnitBlackboard.Target = unit;
                }
            }
        }
        return UnitBlackboard.Target == null ? TaskStatus.Failure : TaskStatus.Success;
    }
}
