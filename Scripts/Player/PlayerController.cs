using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Camera properties
    private Camera _camera;
    [Range(0, 50)]
    public float CameraSpeed;
    private bool _isPointerDown;
    private bool _isCameraMoving;
    private string _previousDirection;
    #endregion

    #region Current room parameters
    public RoomController CurrentRoomController;
    private float _currentRoomBoundsLeft;
    private float _currentRoomBoundsRight;
    #endregion

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();

        _currentRoomBoundsLeft = CurrentRoomController.Background.bounds.min.x + _camera.orthographicSize;
        _currentRoomBoundsRight = CurrentRoomController.Background.bounds.max.x - _camera.orthographicSize;
    }

    private void Update()
    {
        if (_isPointerDown && _isCameraMoving)
        {
            MoveCameraInDirection(_previousDirection);
        }
    }

    public void ChangeRoom(RoomController newRoom)
    {
        CurrentRoomController = newRoom;

        _currentRoomBoundsLeft = CurrentRoomController.Background.bounds.min.x + _camera.orthographicSize;
        _currentRoomBoundsRight = CurrentRoomController.Background.bounds.max.x - _camera.orthographicSize;
    }

    public void MoveCameraInDirection(string direction)
    {
        _previousDirection = direction;
        _isCameraMoving = true;
        Vector3 newCameraPosition = _camera.transform.position;

        if (direction == "Left")
        {
            if (_camera.transform.position.x - _camera.orthographicSize > _currentRoomBoundsLeft)
            {
                newCameraPosition.x -= (CameraSpeed * 0.1f) * Time.deltaTime;
            }
        }
        else if (direction == "Right")
        {
            if (_camera.transform.position.x + _camera.orthographicSize < _currentRoomBoundsRight)
            {
                newCameraPosition.x += (CameraSpeed * 0.1f) * Time.deltaTime;
            }
        }

        _camera.transform.position = newCameraPosition;
    }

    public void TogglePointerClick(bool boolean)
    {
        _isPointerDown = boolean;
    }
}