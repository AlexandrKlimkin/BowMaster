using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{

    public Unit Unit { get; private set; }

    private void Awake() {
        Unit = GetComponent<Unit>();
    }

    void Update()
    {
        Unit.MoveController.Move(1f);
    }

}
