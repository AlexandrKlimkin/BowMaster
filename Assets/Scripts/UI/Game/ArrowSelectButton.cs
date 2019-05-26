using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelectButton : MonoBehaviour, ISelectedElement {
    public int Index;
    private Button _Button;
    public Image CDImage;
    public Image SelectionImage;

    public SelectPanel Panel { get; private set; }

    private void Awake() {
        Panel = GetComponentInParent<SelectPanel>();
        _Button = GetComponent<Button>();
        _Button.onClick.AddListener(SelectArrow);
    }

    private void Update() {
        var bow = (RangeWeapon)PlayerController.Instance.Owner.AttackController.Weapon;
        CDImage.fillAmount = bow.Arrows[Index].NormalizedCD;
    }

    private void SelectArrow() {
        Panel.SelectOne(this);
    }

    public void Select() {
        var weapon = (RangeWeapon)PlayerController.Instance.Owner.AttackController.Weapon;
        weapon.SelectArrow(Index);
        SelectionImage.enabled = true;
    }

    public void UnSelect() {
        SelectionImage.enabled = false;
    }

    private void OnDestroy() {
        _Button.onClick.RemoveAllListeners();
    }
}