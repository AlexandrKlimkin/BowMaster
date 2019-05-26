using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon {

    public List<Arrow> Arrows;
    public Transform FirePoint;
    public float Strength;
    public Arrow SelectedArrow { get; private set; }

    public Vector2 Vector { get; set; }

    protected override void Awake() {
        base.Awake();
        SelectedArrow = Arrows[0];
    }

    public override void OnStartAttack() { }

    public override void Hit() {
        base.Hit();
        if (SelectedArrow.Recharged) {
            var projectile = PoolManager.GetObject(SelectedArrow.Projectile.name).GetComponent<GravityProjectile>();
            SelectedArrow.LastShotTime = Time.time;
            var angle = Vector2.SignedAngle(Vector2.right, Vector);
            var rotation = Quaternion.Euler(0, 0, angle);
            projectile.Speed = Vector.magnitude * Strength;
            projectile.PerformShot(FirePoint.position, rotation, new Damage(SelectedArrow.Damage, Controller.Owner));
        }
    }

    public void SelectArrow(int index) {
        if (Arrows.Count > index)
            SelectedArrow = Arrows[index];
    }
}