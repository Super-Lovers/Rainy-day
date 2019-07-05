using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupBubble : MonoBehaviour
{
    public PopupType Type;
    [Header("Required if popup type is Text! =================")]
    public string GenericMessage;
    public string LockedMessage;

    #region Components
    [Space(10)]
    public InteractableController InteractableController;
    [Header("Required if popup type is Text! =================")]
    public TextMeshProUGUI TextLabel;
    #endregion

    private void Start()
    {
        if (Type == PopupType.Text) {
            TextLabel = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void DisplayLabel()
    {
        Invoke("HidePopup", 3f);

        if (InteractableController.IsObjectLocked)
        {
            TextLabel.text = LockedMessage;
        }
        else if (InteractableController.IsObjectLocked == false)
        {
            TextLabel.text = GenericMessage;
        }
    }

    public void DisplayIcons()
    {
        Invoke("HidePopup", 3f);
    }

    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}