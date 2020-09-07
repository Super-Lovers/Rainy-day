using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    public static InteractablesManager Instance;
    public List<InteractableController> interactables = new List<InteractableController>();

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

    public void UpdateInteractableState(InteractableController interactable, bool new_locked_state)
    {
        var is_match_found = false;
        foreach (var controller in interactables)
        {
            if (controller == interactable)
            {
                is_match_found = true;
                controller.IsObjectLocked = new_locked_state;
                break;
            }
        }

        if (PlayerController.Instance.debug_mode == true && is_match_found == false) {
            Debug.Log($"The interactabl component of <color=#ff0000>{interactable.gameObject.name}</color> is not valid!");
        }
    }
}
