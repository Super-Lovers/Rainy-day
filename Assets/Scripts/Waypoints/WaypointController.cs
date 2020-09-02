using UnityEngine;

public class WaypointController : MonoBehaviour
{
    // Used for room entrances/exits <- they're both the same thing so I used "origin"
    public WaypointType Type;
    [Header("Only used for Origin type waypoints.")]
    public RoomController newRoom;

    [Space(5)]
    public bool isWaypointOccupied;
}
