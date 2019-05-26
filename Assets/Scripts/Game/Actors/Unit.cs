using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Actor {
    public MovementController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }
    public Collider Collider { get; private set; }
    public static List<Unit> ActiveUnits { get; private set; }
    public int TeamIndex;

    protected override void Awake() {
        base.Awake();
        AttackController = GetComponent<AttackController>();
        MoveController = GetComponent<MovementController>();
        Collider = GetComponent<Collider>();
        if (ActiveUnits == null)                 //КОСТЫЛЬ
            ActiveUnits = new List<Unit>();
        ActiveUnits.Add(this);
    }

    public override void Die() {
        base.Die();
        if (Collider != null)
            Collider.enabled = false;
        if(MoveController)
            MoveController.enabled = false;
        if (AttackController)
            AttackController.enabled = false;
        ActiveUnits.Remove(this);
        Destroy(gameObject);
    }
}