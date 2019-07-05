using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour, ICat
{
    [Header("Statistics ====================")]
    #region Statistics
    [SerializeField]
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    [SerializeField]
    private int _nourishment;
    public int Nourishment
    {
        get
        {
            return _nourishment;
        }
        set
        {
            _nourishment = value;
        }
    }
    private int _nourishmentDecayDelay;
    public int NourishmentDecayDelay
    {
        get
        {
            return _nourishmentDecayDelay;
        }
        set
        {
            _nourishmentDecayDelay = value;
        }
    }
    [SerializeField]
    private int _satisfaction;
    public int Satisfaction
    {
        get
        {
            return _satisfaction;
        }
        set
        {
            _satisfaction = value;
        }
    }
    [SerializeField]
    private State _state;
    public State State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
        }
    }
    #endregion

    [Header("Occupations ====================")]
    [Space(10)]
    [SerializeField]
    private RoomController _room;
    public RoomController Room
    {
        get
        {
            return _room;
        }
        set
        {
            _room = value;
        }
    }
    [SerializeField]
    private BowlController _bowl;
    public BowlController Bowl
    {
        get
        {
            return _bowl;
        }
        set
        {
            _bowl = value;
        }
    }
    [SerializeField]
    private ToyController _toy;
    public ToyController Toy
    {
        get
        {
            return _toy;
        }
        set
        {
            _toy = value;
        }
    }
    [SerializeField]
    private WaypointController _currentWaypoint;
    public WaypointController CurrentWaypoint
    {
        get
        {
            return _currentWaypoint;
        }
        set
        {
            _currentWaypoint = value;
        }
    }

    #region Components
    AudioController _audioController;
    Rigidbody2D _rigidBody2D;
    #endregion

    private void Start()
    {
        _audioController = GetComponentInChildren<AudioController>();
        _rigidBody2D = GetComponentInChildren<Rigidbody2D>();

        List<Cat> listOfCats = Cats.Instance.ListOfCats;
        if (listOfCats.Contains(this) == false)
        {
            listOfCats.Add(this);
        }

        Invoke("Test", 0.1f);
    }

    private void Test()
    {
        // Eat();
        // ChangeRoom("Kitchen");
        // CurrentWaypoint = GetNewWaypoint(WaypointType.Generic);
    }

    private void Update()
    {
        // TODO: Coroutines of the core game loop
    }

    public void Eat()
    {
        StartCoroutine(Bite());
    }

    private IEnumerator Bite() {
        yield return new WaitForSeconds(Bowl.Sustenance.TimeToDevour);

        Debug.Log($"<color=#00ff00>{Name}</color> has devoured <color=#ff0000>{Bowl.Sustenance.Name}</color>.");

        // TODO: When the cat finishes eating a substance, play a sound effect.

        NourishmentDecayDelay = Bowl.Sustenance.NourishmentDecayDelay;
        Nourishment += Bowl.Sustenance.NourishmentPoints;
        Bowl.EatSustanence();
    }

    public WaypointController GetNewWaypoint(WaypointType newWaypointType)
    {
        CurrentWaypoint.IsWaypointOccupied = false;

        List<WaypointController> waypointsAvailable = new List<WaypointController>();

        foreach (WaypointController waypoint in Room.Waypoints.ListOfWaypoints)
        {
            if (waypoint.Type == newWaypointType && waypoint.IsWaypointOccupied == false)
            {
                waypointsAvailable.Add(waypoint);
            }
        }

        WaypointController newWaypoint = waypointsAvailable[
            Random.Range(0, waypointsAvailable.Count)];
        newWaypoint.IsWaypointOccupied = true;
        return newWaypoint;
    }

    public void ChangeRoom(string newRoom)
    {
        bool isRoomValid = false;
        foreach (RoomController room in Rooms.Instance.ListOfRooms)
        {
            if (room.Name == newRoom)
            {
                isRoomValid = true;
                Debug.Log($"<color=#00ff00>{Name}</color> moved from room <color=#ffff00>{Room}</color> to <color=#ffff00>{room}</color>.");
                Room = room;
                break;
            }
        }

        if (isRoomValid == false)
        {
            Debug.Log($"The room {newRoom} is not valid!");
        }
    }

    public void Pet()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(State newState)
    {
        Debug.Log($"<color=#00ff00>{Name}</color>'s state has been changed from <color=#ffff00>{_state}</color> to <color=#ffff00>{newState}</color>.");
        _state = newState;
    }
}
