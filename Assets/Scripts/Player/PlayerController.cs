using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public bool debug_mode;

    [Space(10)]
    public RoomController current_room_controller;

    private new Camera camera;

    private AudioController audio_controller;

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
        camera = FindObjectOfType<Camera>();
        audio_controller = GetComponent<AudioController>();
    }

    public void ChangeRoom(string new_room) {

        current_room_controller.entrance_door.Play("DoorOpenAnimation", -1, 0);
        audio_controller.PlaySound("Open Door");

        var is_room_valid = false;
        foreach (var room in Rooms.Instance.rooms)
        {
            if (room.name == new_room)
            {
                is_room_valid = true;

                current_room_controller = room;

                StartCoroutine(ChangeCameraOrigin(current_room_controller.camera_origin));
                Rooms.Instance.StartRoomTransition();

                break;
            }
        }

        if (PlayerController.Instance.debug_mode == true && is_room_valid == false) {
            Debug.Log($"The room ${new_room} is not valid!");
        }
    }

    private IEnumerator ChangeCameraOrigin(Transform newOrigin)
    {
        yield return new WaitForSeconds(1f);
        camera.transform.transform.position = new Vector3(newOrigin.position.x, newOrigin.position.y, camera.transform.position.z);
    }
}