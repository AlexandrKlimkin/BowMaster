using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    private List<ISelectedElement> _Elements = new List<ISelectedElement>();
    public ISelectedElement Selected;

    private void Start() {
        GetComponentsInChildren(_Elements);
        UnselectAll();
        if(_Elements.Count > 0)
            SelectOne(_Elements[0]);
    }

    private void UnselectAll() {
        _Elements.ForEach(_ => _.UnSelect());
    }

    public void SelectOne(ISelectedElement element) {
        if (Selected == element)
            return;
        if(Selected != null)
            Selected.UnSelect();
        element.Select();
        Selected = element;
    }
}
