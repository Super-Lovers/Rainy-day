using UnityEngine;

public class WaypointController : MonoBehaviour
{
    // Used for room entrances/exits <- they're both the same thing so I used "origin"
    public WaypointType type;
    [Header("Only used for Origin type waypoints.")]
    public RoomController new_room;

    [Space(5)]
    public bool is_waypoint_occupied;
}
