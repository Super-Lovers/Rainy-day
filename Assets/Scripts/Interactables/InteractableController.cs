using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour, IInteractable
{
    public List<GameObject> objects_to_activate = new List<GameObject>();
    public List<GameObject> objects_to_deactivate = new List<GameObject>();

    [SerializeField]
    private bool is_object_locked;
    public bool IsObjectLocked
    {
        get
        {
            return is_object_locked;
        }
        set
        {
            is_object_locked = value;
        }
    }

    private void Start()
    {
        var controllers = InteractablesManager.Instance.interactables;

        if (controllers.Contains(this) == false)
        {
            controllers.Add(this);
        }
    }

    public void ToggleObject()
    {
        foreach (var obj in objects_to_activate) {
            obj.SetActive(true);
        }
        foreach (var obj in objects_to_deactivate) {
            obj.SetActive(false);
        }
    }
}
