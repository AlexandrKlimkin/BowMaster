using UnityEngine;
using UnityEngine.UI;

public class MovementButton : MonoBehaviour
{
    public MovementDirection Direction = MovementDirection.Left;

    private PressButton _Button;

    private void Awake()
    {
        _Button = GetComponent<PressButton>();
        _Button.OnPress += OnButtonPress;
        _Button.OnRelease += OnButtonRelease;
    }

    private void OnButtonPress() {
        var direction = Direction == MovementDirection.Left ? -1f : 1f;
        PlayerController.Instance.MovementController.Direction = direction;
    }

    private void OnButtonRelease() {
        PlayerController.Instance.MovementController.Direction = 0;
    }

    private void OnDestroy() {
        _Button.OnPress -= OnButtonPress;
        _Button.OnRelease -= OnButtonRelease;
    }

    public enum MovementDirection
    {
        Left, Right
    }
}
