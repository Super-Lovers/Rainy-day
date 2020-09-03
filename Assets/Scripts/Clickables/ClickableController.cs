using UnityEngine;

public class ClickableController : MouseController
{
    public PlayerController PlayerController;

    public override void Execute(GameObject interactableClicked)
    {
        InteractableController interactableController;
        PropertiesController properties = interactableClicked.GetComponent<PropertiesController>();

        if (properties != null)
        {
            switch (properties.Name)
            {
                default:
                    interactableController = interactableClicked.GetComponent<InteractableController>();
                    interactableController.ToggleObject();
                    break;
                case "Doorway to Kitchen":
                    PlayerController.ChangeRoom("Kitchen");
                    break;
                case "Doorway to Living room":
                    PlayerController.ChangeRoom("Living room");
                    break;
                case "Stove":
                    StoveController stove = interactableClicked.GetComponent<StoveController>();
                    interactableController = interactableClicked.GetComponent<InteractableController>();

                    interactableController.ToggleObject();
                    break;
                case "Bowl Selector":
                    interactableController = interactableClicked.GetComponent<InteractableController>();
                    break;
                case "Cat":
                    var catController = interactableClicked.GetComponentInParent<Cat>();
                    var audioController = catController.audioController;

                    audioController.PlaySound("Purr_short");
                    catController.Pet();
                    break;
            }
        }
    }
}
