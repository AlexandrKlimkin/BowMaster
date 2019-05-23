using UnityEngine;
using UnityEditor;
using UI.Markers;
using UnityEngine.UI;

public class HealthMarkerWidget : MarkerWidget<HealthMarkerData> {

    private Slider _Slider;

    protected override void Awake() {
        base.Awake();
        _Slider = GetComponentInChildren<Slider>();
    }

    protected override void HandleData(HealthMarkerData data) {
        _Slider.value = data.RelativeHealth;
    }
}