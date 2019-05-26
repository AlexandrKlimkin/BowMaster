using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectedElement {
    SelectPanel Panel { get; }
    void Select();
    void UnSelect();
}
