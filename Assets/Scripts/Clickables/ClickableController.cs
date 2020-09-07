using UnityEngine;

public class ClickableController : MouseController
{
    public PlayerController player_controller;

    public override void Execute(GameObject interactable_clicked)
    {
        var properties_controller = interactable_clicked.GetComponent<PropertiesController>();

        if (properties_controller != null)
        {
            switch (properties_controller.name)
            {
                default:
                    interactable_clicked.GetComponent<InteractableController>().ToggleObject();
                    break;
                case "Doorway to Kitchen":
                    player_controller.ChangeRoom("Kitchen");
                    break;
                case "Doorway to Living room":
                    player_controller.ChangeRoom("Living room");
                    break;
                case "Stove":
                    interactable_clicked.GetComponent<InteractableController>().ToggleObject();
                    break;
                case "Cat":
                    var catController = interactable_clicked.GetComponentInParent<Cat>();
                    var audio_controller = catController.audio_controller;

                    audio_controller.PlaySound("Purr_short");
                    catController.Pet();
                    break;
            }
        }
    }
}
