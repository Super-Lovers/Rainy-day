using UnityEngine;
using UnityEngine.UI;

public class MealIconController : MonoBehaviour
{
    private const float HOVER_RADIUS = 1.4f;
    private Image imageComponent;
    private Color initialColor;
    private Color32 hoverColor;

    private AudioController audioController;

    private void Start() {
        audioController = GetComponent<AudioController>();
        imageComponent = GetComponent<Image>();

        initialColor = imageComponent.color;
        hoverColor = new Color32(255, 255, 255, 255);
    }

    public void OnMouseOver() {
        transform.localScale = 
            new Vector3(HOVER_RADIUS, HOVER_RADIUS, HOVER_RADIUS);

        imageComponent.color = hoverColor;
        audioController.PlaySound("UI hover");
    }

    public void OnMouseExit() {
        transform.localScale = Vector3.one;

        imageComponent.color = initialColor;
    }

    public void OnMouseUp() {
        transform.localScale = Vector3.one;

        imageComponent.color = initialColor;
    }
}
