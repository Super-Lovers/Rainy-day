using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    public static InteractablesManager Instance;
    public List<InteractableController> Interactables = new List<InteractableController>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateInteractableState(InteractableController interactable, bool newLockedState)
    {
        bool isMatchFound = false;
        foreach (InteractableController controller in Interactables)
        {
            if (controller == interactable)
            {
                isMatchFound = true;
                controller.IsObjectLocked = newLockedState;
                break;
            }
        }

        if (isMatchFound == false)
        {
            Debug.Log($"The interactabl component of <color=#ff0000>{interactable.gameObject.name}</color> is not valid!");
        }
    }
}
