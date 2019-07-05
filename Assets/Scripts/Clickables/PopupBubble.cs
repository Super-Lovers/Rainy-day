using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupBubble : MonoBehaviour
{
    public string GenericMessage;
    public string LockedMessage;

    #region Components
    [Space(10)]
    public InteractableController InteractableController;
    public TextMeshProUGUI TextLabel;
    #endregion

    private void Start()
    {
        TextLabel = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DisplayLabel()
    {
        Invoke("HideLabel", 3f);

        if (InteractableController.IsObjectLocked)
        {
            TextLabel.text = LockedMessage;
        }
        else if (InteractableController.IsObjectLocked == false)
        {
            TextLabel.text = GenericMessage;
        }
    }

    private void HideLabel()
    {
        gameObject.SetActive(false);
    }
}