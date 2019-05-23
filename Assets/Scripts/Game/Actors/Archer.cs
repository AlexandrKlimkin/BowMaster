using UnityEngine;
using System.Collections;

public class Archer : Unit {
    public BowController BowController { get; private set; }

    protected override void Awake() {
        base.Awake();
        BowController = GetComponentInChildren<BowController>();
    }

    public override void Die() {
        base.Die();
        if (BowController)
            BowController.enabled = false;
    }
}
