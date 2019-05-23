using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBehaviour<MapManager>
{
    public EdgeCollider2D MapCollider { get; private set; }

    private Vector2[] _GroundPoints;

    protected override void Awake() {
        base.Awake();
        MapCollider = GetComponent<EdgeCollider2D>();
        _GroundPoints = MapCollider.points;
    }

    private void Start() {

    }

    public float GetYCoordinate(float x) {
        var localX = (x - transform.position.x);
        int pointIndex = (int)localX;
        var firstPoint = _GroundPoints[pointIndex];
        var secondPoint = pointIndex < (_GroundPoints.Length - 1) ? _GroundPoints[pointIndex + 1] : _GroundPoints[pointIndex - 1];
        var localY = GetValueOnStraight(firstPoint, secondPoint, localX);
        return localY + transform.position.y;
    }

    public Vector2 GetPosition(float x) {
        return new Vector2(x, GetYCoordinate(x));
    }

    private float GetValueOnStraight(Vector2 firstPoint, Vector2 secondPoint, float x) {
        var y = ((x - firstPoint.x) / (secondPoint.x - firstPoint.x)) * (secondPoint.y - firstPoint.y) + firstPoint.y;
        return y;
    }

}