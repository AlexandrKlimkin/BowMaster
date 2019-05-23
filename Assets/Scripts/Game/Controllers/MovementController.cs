using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float MaxSpeed;

    public void Move(float value)
    {
        Mathf.Clamp(value, -1f, 1f);
        var xDelta = value * MaxSpeed * Time.deltaTime;
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
