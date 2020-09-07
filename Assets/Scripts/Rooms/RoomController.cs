using System;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public new string name;
    [Header("Only for the room which has the door facing it")]
    public Animator entrance_door;
    [NonSerialized]
    public Sprite background;
    [Header("Position of the camera entering the room")]
    [Space(10)]
    public Transform camera_origin;
    [Header("Boundaries of the room")]
    [Space(10)]
    public Waypoints waypoints;

    [Space(10)]
    [SerializeField]
    private GameObject lights = default;

    private void Awake()
    {
        background = GetComponentsInChildren<SpriteRenderer>()[0].sprite;
    }

    private void Start() {
        var rooms = Rooms.Instance.rooms;

        if (rooms.Contains(this) == false)
        {
            rooms.Add(this);
        }
    }

    public bool IsLightsOn() {
        return lights.activeSelf;
    }
}
