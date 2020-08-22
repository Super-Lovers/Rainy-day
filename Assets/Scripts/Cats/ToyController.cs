using UnityEngine;

public class ToyController : MonoBehaviour
{
    public string Name;
    public Cat Owner;
    public int TimeToPlay;
    private bool _isDragging;
    public bool IsDragging
    {
        get
        {
            return _isDragging;
        }
        set
        {
            _isDragging = value;
        }
    }

    #region Components
    Rigidbody2D _rigidBody2D;
    #endregion

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDragging)
        {
            Drag();
        }
    }

    public void Drag()
    {
        _isDragging = true;
        _rigidBody2D.gravityScale = 0;
        gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                                    transform.position.z);
    }

    public void Drop()
    {
        _rigidBody2D.gravityScale = 1;
        _isDragging = false;
    }
}