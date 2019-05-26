using UnityEngine;

public class GravityProjectile : Projectile {

    public float Speed { get; set; }
    public float Gravity { get; set; } = 20f;

    private Vector3 _Velocity;
    public bool FriendlyFire;

    //private TrailEffect _Trail;

    protected override void OnEnable() {
        base.OnEnable();
        _Velocity = this.transform.right * Speed;
        //_Trail = VisualEffect.GetEffect<TrailEffect>("Trail" + gameObject.name.Replace("(Clone)", string.Empty));
        //if (_Trail != null) {
        //    _Trail.Attach(this.transform);
        //    _Trail.Play();
        //}
    }

    protected override void KillProjectile() {
        //if (_Trail != null)
        //    _Trail.Detach();
        base.KillProjectile();
    }

    public override void PerformShot(Vector3 position, Quaternion rotation, Damage damage/*, int shotTimeStamp, bool compensateSpawnLag*/) {
        base.PerformShot(position, rotation, damage/*, shotTimeStamp, compensateSpawnLag*/);
        _Velocity = this.transform.right * Speed;
        //_Trail = VisualEffect.GetEffect<TrailEffect>("Trail" + gameObject.name.Replace("(Clone)", string.Empty));
        //if (_Trail != null) {
        //    _Trail.Attach(this.transform);
        //    _Trail.Play();
        //}
    }

    public override void PerformHit(IDamageable target) {
        //ProjectileDamage.EffectType = PackIndex == 0 ? EffectType.Big : EffectType.None;
        //ProjectileDamage.WeaponType = WeaponType.Turret;
        base.PerformHit(target);
    }

    protected override void SimulateStep(float deltaTime) {
        var delta = _Velocity * deltaTime;
        var targetPosition = this.transform.position + delta;
        var deltaRay = new Ray(this.transform.position, delta);
        var hit = Physics2D.Raycast(transform.position, delta, delta.magnitude, Layers.Masks.Damageable);
        if (hit.collider != null) {
            if (FriendlyFire) {
                var target = hit.transform.GetComponent<IDamageable>();
                    PerformHit(target);
            }
            else {
                if (!hit.transform.IsChildOf(ProjectileDamage.Instigator.transform)) {
                    var target = hit.transform.GetComponent<IDamageable>();
                        //ProjectileDamage.Position = hit.point;
                        //ProjectileDamage.Normal = hit.normal;
                    PerformHit(target);
                }
            }
        }
        //if (targetPosition.y < 0) {
        //    ProjectileDamage.Position = this.transform.position;
        //    PerformHit(WaterPlane.Instance);
        //}
        this.transform.position = targetPosition;
        AllignDirection(_Velocity);
        _Velocity -= Gravity * Vector3.up * deltaTime;
        if (transform.position.y < 0)
            KillProjectile();
    }

    private void AllignDirection(Vector3 velocity) {
        var angle = Vector2.SignedAngle(Vector2.right, _Velocity);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}