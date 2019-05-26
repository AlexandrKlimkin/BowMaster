using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float Speed;
    public float Direction;

    private void Update() {
        Direction = (Direction > 0) ? 1 : ((Direction < 0) ? -1f : 0f);
        Move(Direction);
    }

    private void Move(float value)
    {
        if (value == 0)
            return;
        Mathf.Clamp(value, -1f, 1f);
        var xDelta = value * Speed * Time.deltaTime;
        var x = transform.position.x;
        x += xDelta;
        x = Mathf.Clamp(x, 0, 100f);
        transform.position = MapManager.Instance.GetPosition(x);
    }

    public void Teleport(float x) {
        x = Mathf.Clamp(x, 0, 100f);
        transform.position = MapManager.Instance.GetPosition(x);
    }

}
