using UnityEngine;

public class ClickableController : MouseController
{
    public PlayerController PlayerController;

    public override void Execute(GameObject interactableClicked)
    {
        PropertiesController properties = interactableClicked.GetComponent<PropertiesController>();

        switch (properties.Name)
        {
            case "Doorway to Kitchen":
                PlayerController.ChangeRoom("Kitchen");
                break;
            case "Doorway to Living room":
                PlayerController.ChangeRoom("Living room");
                break;
            case "Bowl":
                InteractableController interactableController = interactableClicked.GetComponent<InteractableController>();
                if (interactableController.PopupBubble.gameObject.activeSelf == false)
                {
                    interactableController.PopupBubble.gameObject.SetActive(true);
                    interactableController.PopupBubble.DisplayLabel();
                }
                break;
        }
    }
}
