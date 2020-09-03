using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour, IInteractable
{
    public List<GameObject> ObjectsToActivate = new List<GameObject>();
    public List<GameObject> ObjectsToDeactivate = new List<GameObject>();

    [SerializeField]
    private bool _isObjectLocked;
    public bool IsObjectLocked
    {
        get
        {
            return _isObjectLocked;
        }
        set
        {
            _isObjectLocked = value;
        }
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
        foreach (GameObject obj in ObjectsToActivate) {
            obj.SetActive(true);
        }
        foreach (GameObject obj in ObjectsToDeactivate) {
            obj.SetActive(false);
        }
    }
}
