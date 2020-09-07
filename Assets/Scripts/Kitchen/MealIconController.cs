using UnityEngine;
using UnityEngine.UI;

public class MealIconController : MonoBehaviour
{
    private const float HOVER_RADIUS = 1.4f;
    private Image image_component;
    private Color initial_color;
    private Color32 hover_color;

    private AudioController audio_controller;

    private void Start() {
        audio_controller = GetComponent<AudioController>();
        image_component = GetComponent<Image>();

        initial_color = image_component.color;
        hover_color = new Color32(255, 255, 255, 255);
    }

    public void OnMouseOver() {
        transform.localScale = 
            new Vector3(HOVER_RADIUS, HOVER_RADIUS, HOVER_RADIUS);

        image_component.color = hover_color;
        audio_controller.PlaySound("UI hover");
    }

    public void OnMouseExit() {
        transform.localScale = Vector3.one;

        image_component.color = initial_color;
    }

    public void OnMouseUp() {
        transform.localScale = Vector3.one;

        image_component.color = initial_color;
    }
}
