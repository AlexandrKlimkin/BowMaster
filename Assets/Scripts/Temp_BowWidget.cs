using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Temp_BowWidget : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform Target;
    public LineRenderer Line;
    public RectTransform BoundsTransform;
    private AttackController _AttackController;

    private Camera _MainCamera;
    private bool _Dragging;
    private Vector2 _GrabDelta;

    private void Start()
    {
        _MainCamera = Camera.main;
        BoundsTransform.gameObject.SetActive(false);
        _AttackController = PlayerController.Instance.AttackController;
    }

    private void Update()
    {
        if (PlayerController.Instance)
        {
            if (!_Dragging)
                FollowTarget(transform);
            FollowTarget(BoundsTransform);
            DrawLine();
        }
    }

    private void FollowTarget(Transform tr)
    {
        tr.position = _MainCamera.WorldToScreenPoint(Target.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var position = eventData.position + _GrabDelta;
        var delta = position - (Vector2)BoundsTransform.position;
        var distance = delta.magnitude;
        if (distance > BoundsTransform.rect.size.x / 2) {
            transform.position = BoundsTransform.position + (Vector3)delta.normalized * BoundsTransform.rect.size.x / 2;
        }
        else
            transform.position = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _Dragging = true;
        BoundsTransform.gameObject.SetActive(true);
        _GrabDelta = (Vector2)transform.position - eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _Dragging = false;
        BoundsTransform.gameObject.SetActive(false);
        var dist = Target.position - _MainCamera.ScreenToWorldPoint(transform.position);
        Fire(dist);
    }

    private void DrawLine()
    {
        var pos1 = _MainCamera.ScreenToWorldPoint(transform.position);
        var pos2 = Target.position;
        var positions = new Vector3[] { pos1, pos2 };
        Line.SetPositions(positions);
    }

    private void Fire(Vector2 vector)
    {
        var weapon = (RangeWeapon)_AttackController.Weapon;
        weapon.Vector = vector;
        _AttackController.PerformHit();
    }
}
