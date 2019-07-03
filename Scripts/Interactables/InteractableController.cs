using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour, IInteractable
{
    public GameObject ObjectToToggle
    {
        get { return ObjectToToggle; }
        set { ObjectToToggle = value; }
    }
    public bool IsObjectLocked
    {
        get { return IsObjectLocked; }
        set { IsObjectLocked = value; }
    }

    private void Start()
    {
        List<InteractableController> controllers = InteractablesManager.Instance.Interactables;

        if (controllers.Contains(this) == false)
        {
            controllers.Add(this);
        }
    }

    public void ToggleObject()
    {
        ObjectToToggle.SetActive(true);
        gameObject.SetActive(false);
    }
}
