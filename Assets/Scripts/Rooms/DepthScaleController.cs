using UnityEngine;

public class DepthScaleController : MonoBehaviour
{
    private bool _isRising;
    public bool IsRising {
        get {
            return _isRising;
        }
        set {
            _isRising = value;
        }
    }

    Cat _catScript;
    ToyController _toyController;

    private void Start() {
        if (GetComponent<ToyController>() != null) {
            _toyController = GetComponent<ToyController>();
        }
        if (GetComponent<Cat>() != null) {
            _catScript = GetComponent<Cat>();
        }
    }

    private void Update() {
        RoomController currentRoom = PlayerController.Instance.CurrentRoomController;
        float newScale = 1 - (Mathf.Abs(transform.position.y - currentRoom.transform.position.y) * 0.65f);

        if (_toyController != null && _toyController.IsDragging == false) {
            if (_isRising == false && transform.position.y - currentRoom.transform.position.y < PlayerController.Instance.CurrentRoomController.DepthLimit) {
                transform.localScale = new Vector3(newScale, newScale, 1);
            }
        }

        if (_catScript != null && _catScript.IsClimbed == false) {
            if (_isRising == false && transform.position.y - currentRoom.transform.position.y < _catScript.Room.DepthLimit) {
                transform.localScale = new Vector3(newScale, newScale, 1);
            }
        }
    }
}
