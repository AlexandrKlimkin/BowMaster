using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public Unit Owner { get; private set; }
    public Arrow[] Arrows;
    public Transform FirePoint;
    public float Strength;

    public Arrow SelectedArrow { get; private set; }

    private string _ProjectilePoolId;

    private void Awake() {
        Owner = GetComponent<Unit>();
        SelectedArrow = Arrows[0];
    }

    public void Attack(Vector2 vector)
    {
        //var arrow = Instantiate(SelectedArrow.Projectile) as GravityProjectile; //ToDo ObjectPool
        var arrow = GameObjectPoolContainer.Pop<>()

        var angle = Vector2.SignedAngle(Vector2.right, vector);
        var rotation = Quaternion.Euler(0, 0, angle);
        arrow.Speed = vector.magnitude * Strength;
        arrow.PerformShot(FirePoint.position, rotation, new Damage(50f, Owner));
    }

    public void SelectArrow(int index) {
        if (Arrows.Length > index)
            SelectedArrow = Arrows[index];
    }
}