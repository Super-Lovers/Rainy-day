using UnityEngine;
using System;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    public string Name;
    [Header("Only for the room which has the door facing it")]
    public Animator EntranceDoor;
    [NonSerialized]
    public Sprite Background;
    [Header("Position of the camera entering the room")]
    [Space(10)]
    public Transform CameraOrigin;
    [Header("Boundaries of the room")]
    [Space(10)]
    public Waypoints Waypoints;

    [Space(10)]
    [SerializeField]
    private GameObject lights;

    private void Awake()
    {
        Background = GetComponentsInChildren<SpriteRenderer>()[0].sprite;
    }

    private void Start() {
        List<RoomController> listOfRooms = Rooms.Instance.ListOfRooms;
        if (listOfRooms.Contains(this) == false)
        {
            listOfRooms.Add(this);
        }
    }

    public bool IsLightsOn() {
        return lights.activeSelf;
    }
}
