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

    public void ToggleObject()
    {
        ObjectToToggle.SetActive(true);
        gameObject.SetActive(false);
    }
}
