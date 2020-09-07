using UnityEngine;

public abstract class MouseController : MonoBehaviour
{
    public LayerMask interactables_layer;
    private RaycastHit2D hit_interactable;

    // Toy components
    private ToyController toy_controller;
    private SpriteRenderer toy_renderer;

    protected void Update()
    {
        var mouse_position = 
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            hit_interactable = Physics2D.Raycast(
                mouse_position,
                Vector3.forward,
                Mathf.Infinity,
                interactables_layer);

            if (hit_interactable.collider != null)
            {
                if (toy_renderer != null) {
                    if (toy_controller.is_dragging == true) {
                        toy_controller.Drop();
                    } else if (toy_controller.is_dragging == false) {
                        toy_controller.Drag();
                    }
                } else {
                    Execute(hit_interactable.collider.gameObject);
                }
            }
        } else if (Input.GetMouseButtonDown(1))
        {
            hit_interactable = Physics2D.Raycast(
                mouse_position,
                Vector3.forward,
                Mathf.Infinity,
                interactables_layer);

            if (hit_interactable.collider != null)
            {
                if (hit_interactable.collider.gameObject.GetComponentInParent<Cat>() != null) {
                    //hit_interactable.collider.gameObject.GetComponentInParent<Cat>().Pet();
                }
            }
        } else {
            hit_interactable = Physics2D.Raycast(
                mouse_position,
                Vector3.forward,
                Mathf.Infinity,
                interactables_layer);

            if (hit_interactable.collider != null)
            {
                if (hit_interactable.collider.GetComponent<ToyController>() != null) {
                    toy_renderer = hit_interactable.collider.gameObject.GetComponent<SpriteRenderer>();
                    toy_controller = hit_interactable.collider.gameObject.GetComponent<ToyController>();
                    toy_renderer.color = new Color(255, 255, 255, 0.5f);
                }
            } else {
                if (toy_renderer != null) {
                    toy_renderer.color = new Color(255, 255, 255, 1f);
                    toy_controller = null;
                    toy_renderer = null;
                }
            }
        }
    }

    public abstract void Execute(GameObject interactableClicked);
}