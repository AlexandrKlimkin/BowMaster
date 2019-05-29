using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner_TMP : MonoBehaviour {
    public float TimerPeriod = 8f;
    public GameObject UnitPrefab;

    private float _Timer;

    void Start() {
        _Timer = TimerPeriod;
    }

    void Update() {
        if (_Timer >= TimerPeriod) {
            SpawnSquad();
            _Timer = 0f;
        }
        _Timer += Time.deltaTime;
    }


    private void SpawnSquad() {
        for (int i = 0; i < transform.childCount; i++) {
            var spawnPoint = transform.GetChild(i);
            Instantiate(UnitPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < transform.childCount; i++) {
            var spawnPoint = transform.GetChild(i);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
        }
    }
}
