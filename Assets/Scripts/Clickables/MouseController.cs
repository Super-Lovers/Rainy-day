using UnityEngine;

public abstract class MouseController : MonoBehaviour
{
    public LayerMask InteractablesLayer;

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = 
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hitInteractable;

            Debug.DrawRay(mousePosition, Vector3.forward);
            hitInteractable = Physics2D.Raycast(
                mousePosition,
                Vector3.forward,
                Mathf.Infinity,
                InteractablesLayer);

            if (hitInteractable.collider != null)
            {
                Execute(hitInteractable.collider.gameObject);
            }
        }
    }

    public abstract void Execute(GameObject interactableClicked);
}