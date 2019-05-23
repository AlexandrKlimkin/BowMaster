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
    }

    private void OnButtonPress() {
        var direction = Direction == MovementDirection.Left ? -1f : 1f;
        PlayerController.Instance.MovementController.Move(direction);
    }

    private void OnDestroy() {
        _Button.OnPress -= OnButtonPress;
    }

    public enum MovementDirection
    {
        Left, Right
    }
}
