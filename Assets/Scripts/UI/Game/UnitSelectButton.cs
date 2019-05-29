using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectButton : MonoBehaviour {
    private Button _Button;
    public Image CDImage;

    public Unit Unit;
    public Transform SpawnTransform;
    public float Cd;
    private float _SpawnTime = float.NegativeInfinity;


    private void Awake() {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(SpawnUnit);
    }

    private void Update()
    {
        CDImage.fillAmount = Mathf.Clamp01(1 - (_SpawnTime - Time.time) / Cd);
    }

    private void SpawnUnit()
    {
        if (Time.time > _SpawnTime)
        {
            Instantiate(Unit, SpawnTransform.position, Quaternion.identity);
            _SpawnTime = Time.time + Cd;
        }
    }

    private void OnDestroy() {
        _Button.onClick.RemoveAllListeners();
    }
}