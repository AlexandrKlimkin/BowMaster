using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonBehaviour<PlayerController>
{
    public Archer Unit { get; private set; }
    public MovementController MovementController { get; private set; }
    public BowController AttackController { get; private set; }

    private Camera _MainCamera;

    protected override void Awake() {
        Unit = GetComponent<Archer>();
        MovementController = GetComponent<MovementController>();
        AttackController = GetComponent<BowController>();
    }

    private void Start() {
        _MainCamera = Camera.main;
        ScreenInputWidget.Instance.ClickEvent += FireAtPoint;
    }

//    private void Update() {
//#if UNITY_EDITOR
//        var xAxis = Input.GetAxis("Horizontal");
//        if (xAxis != 0) {
//            MovementController.Move(xAxis);
//        }
//#endif
//    }

    private void FireAtPoint(Vector2 point) {
        var delta = _MainCamera.ScreenToWorldPoint(point) - Unit.BowController.FirePoint.position;
        Unit.BowController.Attack(delta.normalized * 10f);
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        if(ScreenInputWidget.Instance)
            ScreenInputWidget.Instance.ClickEvent -= FireAtPoint;
    }
}