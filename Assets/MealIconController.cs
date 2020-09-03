using UnityEngine;

public class MealIconController : MonoBehaviour
{
    private const float HOVER_RADIUS = 1.4f;
    public void OnMouseOver() {
        transform.localScale = 
            new Vector3(HOVER_RADIUS, HOVER_RADIUS, HOVER_RADIUS);
    }

    public void OnMouseExit() {
        transform.localScale = Vector3.one;
    }
}
