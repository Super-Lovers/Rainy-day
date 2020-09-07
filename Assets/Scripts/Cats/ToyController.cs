using UnityEngine;

public class ToyController : MonoBehaviour
{
    public int time_to_play;
    public bool is_dragging;

    private new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (is_dragging)
        {
            Drag();
        }
    }

    public void Drag()
    {
        is_dragging = true;
        rigidbody.gravityScale = 0;
        gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                                    transform.position.z);
    }

    public void Drop()
    {
        rigidbody.gravityScale = 1;
        is_dragging = false;
    }
}