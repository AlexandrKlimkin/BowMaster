using System.Collections.Generic;
using UnityEngine;

public class BowController : AttackController
{
    public List<Arrow> Arrows;
    public Transform FirePoint;
    public float Strength;

    public Arrow SelectedArrow { get; private set; }

    private string _ProjectilePoolId;

    protected override void Awake() {
        base.Awake();
        SelectedArrow = Arrows[0];
    }

    public void SelectArrow(int index) {
        if (Arrows.Count > index)
            SelectedArrow = Arrows[index];
    }
}