using UnityEngine;

public abstract class MouseController : MonoBehaviour
{
    public LayerMask InteractablesLayer;
    private RaycastHit2D hitInteractable;

    #region Toy components
    private ToyController _toyController;
    private SpriteRenderer _toyRenderer;
    #endregion

    protected void Update()
    {
        Vector3 mousePosition = 
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            hitInteractable = Physics2D.Raycast(
                mousePosition,
                Vector3.forward,
                Mathf.Infinity,
                InteractablesLayer);

            if (hitInteractable.collider != null)
            {
                if (_toyRenderer != null) {
                    if (_toyController.IsDragging == true) {
                        _toyController.Drop();
                    } else if (_toyController.IsDragging == false) {
                        _toyController.Drag();
                    }
                } else {
                    Execute(hitInteractable.collider.gameObject);
                }
            }
        } else {
            hitInteractable = Physics2D.Raycast(
                mousePosition,
                Vector3.forward,
                Mathf.Infinity,
                InteractablesLayer);

            if (hitInteractable.collider != null)
            {
                if (hitInteractable.collider.GetComponent<ToyController>() != null) {
                    _toyRenderer = hitInteractable.collider.gameObject.GetComponent<SpriteRenderer>();
                    _toyController = hitInteractable.collider.gameObject.GetComponent<ToyController>();
                    _toyRenderer.color = new Color(255, 255, 255, 0.5f);
                }
            } else {
                if (_toyRenderer != null) {
                    _toyRenderer.color = new Color(255, 255, 255, 1f);
                    _toyController = null;
                    _toyRenderer = null;
                }
            }
        }
    }

    public abstract void Execute(GameObject interactableClicked);
}