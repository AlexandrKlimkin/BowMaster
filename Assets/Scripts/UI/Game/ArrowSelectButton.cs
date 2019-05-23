using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelectButton : MonoBehaviour
{
    public int Index;
    private Button _Button;

    private void Awake() {
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(SelectArrow);
    }

    private void SelectArrow() {
        PlayerController.Instance.Unit.BowController.SelectArrow(Index);
    }

    private void OnDestroy() {
        _Button.onClick.RemoveAllListeners();
    }
}