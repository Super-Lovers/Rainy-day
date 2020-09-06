using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Camera _camera;
    [Range(0, 50)]
    public float CameraSpeed;
    private bool _isPointerDown;
    private bool _isCameraMoving;
    private string _previousDirection;

    [Space(10)]
    public RoomController CurrentRoomController;
    private float _currentRoomBoundsLeft;
    private float _currentRoomBoundsRight;
    private bool _isCameraTransitioning;

    private RoomController _previousRoom;
    private AudioController audioController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
        audioController = GetComponent<AudioController>();
    }

    private void Update()
    {
        if (_isPointerDown && _isCameraMoving)
        {
            MoveCameraInDirection(_previousDirection);
        }
    }

    public void ChangeRoom(string newRoom) {

        CurrentRoomController.EntranceDoor.Play("DoorOpenAnimation", -1, 0);
        audioController.PlaySound("Open Door");

        bool isRoomValid = false;
        foreach (RoomController room in Rooms.Instance.ListOfRooms)
        {
            if (room.Name == newRoom)
            {
                _isCameraTransitioning = true;
                isRoomValid = true;

                _previousRoom = CurrentRoomController;
                CurrentRoomController = room;

                StartCoroutine(ChangeCameraOrigin(CurrentRoomController.CameraOrigin));
                Rooms.Instance.StartRoomTransition();

                break;
            }
        }

        if (isRoomValid == false)
        {
            Debug.Log($"The room ${newRoom} is not valid!");
        }
    }

    private IEnumerator ChangeCameraOrigin(Transform newOrigin)
    {
        yield return new WaitForSeconds(1f);
        _camera.transform.transform.position = new Vector3(newOrigin.position.x, newOrigin.position.y, _camera.transform.position.z);
        _isCameraTransitioning = false;
    }

    public void MoveCameraInDirection(string direction)
    {
        if (_isCameraTransitioning == false)
        {
            _previousDirection = direction;
            _isCameraMoving = true;
            Vector3 newCameraPosition = _camera.transform.position;

            if (direction == "Left")
            {
                if (_camera.transform.position.x - _camera.orthographicSize > _currentRoomBoundsLeft)
                {
                    newCameraPosition.x -= (CameraSpeed * 2) * Time.deltaTime;
                }
            }
            else if (direction == "Right")
            {
                if (_camera.transform.position.x + _camera.orthographicSize < _currentRoomBoundsRight)
                {
                    newCameraPosition.x += (CameraSpeed * 2) * Time.deltaTime;
                }
            }

            _camera.transform.position = newCameraPosition;
        }
    }

    public void TogglePointerClick(bool boolean)
    {
        _isPointerDown = boolean;
    }
}