using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnPress;
    public event Action OnRelease;
    private bool _Pressed;

    private void Update() {
        if (_Pressed)
            OnPress?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData) {
        _Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        _Pressed = false;
        OnRelease?.Invoke();
    }
}
