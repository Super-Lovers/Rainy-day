using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour, IInteractable
{
    public GameObject ObjectToToggle
    {
        get
        {
            return ObjectToToggle;
        }
        set
        {
            ObjectToToggle = value;
        }
    }
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

    public PopupBubble PopupBubble;

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
