using UnityEngine;

public class ToyController : MonoBehaviour
{
    public string Name;
    public int TimeToPlay;
    public bool isDragging;

    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDragging)
        {
            Drag();
        }
    }

    public void Drag()
    {
        isDragging = true;
        _rigidBody2D.gravityScale = 0;
        gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                                    transform.position.z);
    }

    public void Drop()
    {
        _rigidBody2D.gravityScale = 1;
        isDragging = false;
    }
}