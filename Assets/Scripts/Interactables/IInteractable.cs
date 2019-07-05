using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool IsObjectLocked { get; set; }
    void ToggleObject();
}