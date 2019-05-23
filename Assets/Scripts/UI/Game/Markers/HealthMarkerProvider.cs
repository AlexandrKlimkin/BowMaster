﻿using UnityEngine;
using System.Collections;
using UI.Markers;
using System;

public class HealthMarkerProvider : MarkerProvider<HealthMarkerWidget, HealthMarkerData> {

    public Actor Actor;

    public override bool GetVisibility() {
        return true;
    }

    protected override void RefreshData(HealthMarkerData data) {
        data.WorldPosition = transform.position;
        data.Health = Actor.Health;
        data.RelativeHealth = Actor.RelativeHealth;
    }
}
