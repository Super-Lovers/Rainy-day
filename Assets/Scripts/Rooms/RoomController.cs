using UnityEngine;
using System;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    public string Name;
    [NonSerialized]
    public Sprite Background;
    [Header("Position of the camera entering the room")]
    [Space(10)]
    public Transform CameraOrigin;
    [Header("Boundaries of the room")]
    public Transform LeftBoundary;
    public Transform RightBoundary;
    [Space(10)]
    public Waypoints Waypoints;

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
}
