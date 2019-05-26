using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    //public float LifeTime = 3;
    public int PackIndex { get; set; }
    public Damage ProjectileDamage { get; private set; }
    //public float NormalizedLifeTime { get { return (PhotonNetwork.ServerTimestamp - BirthTimeStamp) / LifeTimeMilliseconds; } }
    protected int BirthTimeStamp { get; private set; }
    //protected float LifeTimeMilliseconds { get; private set; }
    protected virtual float StepTime { get { return Time.fixedDeltaTime; } }
    protected bool Initialized;

    protected virtual void Awake() {
    }

    protected virtual void Update() {
        if (!Initialized)
            return;
        SimulateStep(Time.deltaTime);
        //if (NormalizedLifeTime >= 1) {
        //    KillProjectile();
        //    return;
        //}
    }

    //private void CompensateSpawnLag(float spawnTimeLag) {
    //    var stepTime = StepTime;
    //    var stepCount = (int)(spawnTimeLag / stepTime);

    //    for (var i = 0; i < stepCount; i++) {
    //        SimulateStep(stepTime);
    //        if (!gameObject.activeSelf)
    //            break;
    //    }
    //}

    protected abstract void SimulateStep(float deltaTime);

    protected virtual void OnEnable() {
        //LifeTimeMilliseconds = LifeTime * Constants.MillisecondsInSecond;
        Initialized = false;
    }

    protected virtual void OnDisable() {
    }

    protected virtual void KillProjectile() {
        this.gameObject.GetComponent<PoolObject>().ReturnToPool();
        Initialized = false;
    }

    public virtual void PerformHit(IDamageable target) {
        if (target != null)
            target.TakeDamage(ProjectileDamage);
        KillProjectile();
    }

    public virtual void PerformShot(Vector3 position, Quaternion rotation, Damage damage/*, int shotTimeStamp, bool compensateSpawnLag*/) {
        //BirthTimeStamp = shotTimeStamp;
        //var spawnLag = (PhotonNetwork.ServerTimestamp - BirthTimeStamp) / Constants.MillisecondsInSecond;
        //if (LifeTime < spawnLag) {
        //    KillProjectile();
        //    return;
        //}
        this.transform.position = position;
        this.transform.rotation = rotation;
        this.gameObject.SetActive(true);
        ProjectileDamage = damage;
        //if (compensateSpawnLag)
        //    CompensateSpawnLag(spawnLag);
        Initialized = gameObject.activeSelf;
    }
}