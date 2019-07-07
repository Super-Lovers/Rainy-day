using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Range(0, 100)]
    [SerializeField]
    private int _movementSpeed;
    public int MovementSpeed
    {
        get
        {
            return _movementSpeed;
        }
        set
        {
            _movementSpeed = value;
        }
    }
    [Space(10)]
    public CatDisplay Display;
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
            Display.UpdateSliders();
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
            Display.UpdateSliders();
        }
    }
    [Header("Face and Mood ====================")]
    [Space(10)]
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
    public Image FaceIcon;
    public List<ExpressionController> Expressions = new List<ExpressionController>();
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
    InteractableController _interactableController;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    #endregion

    // State conditions
    private bool _isPlaying;
    private bool _isDisplayingMood;
    private bool _isEating;
    private bool _isMoving;
    private bool _isIdle;
    private bool _isAnimationComplete = true;
    public bool IsClimbed;

    // Counters
    private int _timesPlayerIgnoredCat;
    private int _timeBetweenPets;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _audioController = GetComponentInChildren<AudioController>();
        _rigidBody2D = GetComponentInChildren<Rigidbody2D>();
        _interactableController = GetComponentInChildren<InteractableController>();

        List<Cat> listOfCats = Cats.Instance.ListOfCats;
        if (listOfCats.Contains(this) == false)
        {
            listOfCats.Add(this);
        }

        Display.UpdateSliders();
    }

    private void Update()
    {
        switch (State)
        {
            case State.Hungry:
                if (Room.Name != "Kitchen")
                {
                    if (CurrentWaypoint.Type != WaypointType.Origin)
                    {
                        CurrentWaypoint = GetNewWaypoint(WaypointType.Origin);
                    }

                    if (Vector2.Distance(transform.position, CurrentWaypoint.transform.position) > 0.2f)
                    {
                        SetCatAnimationTo("IsWalking");
                        MoveTo(CurrentWaypoint.gameObject);
                    }
                    else
                    {
                        _isMoving = false;
                        SetCatAnimationTo("IsIdle");
                        ChangeRoom("Kitchen");
                    }
                }
                else if (Room.Name == "Kitchen")
                {
                    if (Vector2.Distance(transform.position, Bowl.transform.position) > 0.4f)
                    {
                        SetCatAnimationTo("IsWalking");
                        MoveTo(Bowl.gameObject);
                    }
                    else
                    {
                        _isMoving = false;
                        SetCatAnimationTo("IsIdle");
                        if (Bowl.Sustenance != null)
                        {
                            if (_isEating == false)
                            {
                                SetCatAnimationTo("IsEating");
                                Eat();
                                _isEating = true;
                            }
                        }
                        else
                        {
                            if (_isDisplayingMood == false)
                            {
                                DisplayMood(State.Hungry);
                            }
                        }
                    }
                }
                break;
            case State.Relaxing:
                if (_isMoving == false)
                {
                    if (_isIdle == false)
                    {
                        if (Random.Range(0, 100) > 89)
                        {
                            CurrentWaypoint = GetNewWaypoint(WaypointType.Origin);
                        }
                        else
                        {
                            CurrentWaypoint = GetNewWaypoint(WaypointType.Generic);
                        }

                        Invoke("ResetMovement", Random.Range(6, 15));
                        _isIdle = true;
                    }
                }

                if (Vector2.Distance(transform.position, CurrentWaypoint.transform.position) > 0.2f)
                {
                    SetCatAnimationTo("IsWalking");
                    MoveTo(CurrentWaypoint.gameObject);
                }
                else
                {
                    SetCatAnimationTo("IsRelaxing");
                    _isMoving = false;
                }
                break;
            case State.Bored:
                if (_isMoving == false)
                {
                    if (_isIdle == false)
                    {
                        if (Random.Range(0, 100) > 89)
                        {
                            CurrentWaypoint = GetNewWaypoint(WaypointType.Origin);
                        }
                        else
                        {
                            CurrentWaypoint = GetNewWaypoint(WaypointType.Generic);
                        }

                        if (_timesPlayerIgnoredCat == 2)
                        {
                            State = State.Angry;
                        }

                        _timesPlayerIgnoredCat++;
                        Invoke("ResetMovement", Random.Range(1, 6));
                        _isIdle = true;
                    }
                }

                if (Vector2.Distance(transform.position, CurrentWaypoint.transform.position) > 0.2f)
                {
                    SetCatAnimationTo("IsWalking");
                    // TODO: Change rooms for origin points
                    MoveTo(CurrentWaypoint.gameObject);
                }
                else
                {
                    SetCatAnimationTo("IsIdle");
                    _isMoving = false;
                }
                break;
            case State.Happy:
                if (_isDisplayingMood == false)
                {
                    DisplayMood(State.Happy);
                }
                break;
            case State.Angry:
                if (_isDisplayingMood == false)
                {
                    DisplayMood(State.Angry);
                }
                break;
            default:
                break;
        }
    }

    public void Eat()
    {
        StartCoroutine(Bite());
    }

    private void SetCatAnimationTo(string animation)
    {
        if (_isAnimationComplete == true)
        {
            switch (animation)
            {
                case "IsWalking":
                    _animator.SetBool("IsWalking", true);

                    _animator.SetBool("IsIdle", false);
                    _animator.SetBool("IsRelaxing", false);
                    _animator.SetBool("IsEating", false);
                    break;
                case "IsIdle":
                    _animator.SetBool("IsIdle", true);

                    _animator.SetBool("IsWalking", false);
                    _animator.SetBool("IsRelaxing", false);
                    _animator.SetBool("IsEating", false);
                    break;
                case "IsEating":
                    _animator.SetBool("IsEating", true);

                    _animator.SetBool("IsWalking", false);
                    _animator.SetBool("IsIdle", false);
                    _animator.SetBool("IsRelaxing", false);
                    break;
                case "IsRelaxing":
                    _animator.SetBool("IsRelaxing", true);

                    _animator.SetBool("IsWalking", false);
                    _animator.SetBool("IsIdle", false);
                    _animator.SetBool("IsEating", false);
                    break;
            }
        }
    }

    private void MoveTo(GameObject obj)
    {
        _isMoving = true;
        if (transform.position.x < obj.transform.position.x) {
            _spriteRenderer.flipX = true;
        } else if (transform.position.x > obj.transform.position.x) {
            _spriteRenderer.flipX = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, obj.transform.position, (MovementSpeed * Time.deltaTime) * 0.015f);
    }

    private void ResetMovement()
    {
        _isMoving = false;
        _isIdle = false;
    }

    public void DisplayMood(State state)
    {
        Invoke("ResetMoodTimer", 5f);
        _isDisplayingMood = true;
        _interactableController.PopupBubble.gameObject.SetActive(true);

        if (state == State.Angry)
        {
            Satisfaction--;
        }

        foreach (ExpressionController mood in Expressions)
        {
            if (mood.Expression == State)
            {
                FaceIcon.sprite =
                mood.Variations[Random.Range(0, mood.Variations.Count)];
            }
        }

        _interactableController.PopupBubble.DisplayIcons();
    }

    private void ResetMoodTimer()
    {
        _isDisplayingMood = false;
    }

    private IEnumerator Bite()
    {
        yield return new WaitForSeconds(Bowl.Sustenance.TimeToDevour);

        _isEating = false;
        Debug.Log($"<color=#000080ff>{Name}</color> has devoured <color=#ff0000>{Bowl.Sustenance.Name}</color>.");

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
        if (Toy == null)
        {
            bool isRoomValid = false;
            foreach (RoomController room in Rooms.Instance.ListOfRooms)
            {
                if (room.Name == newRoom)
                {
                    isRoomValid = true;

                    foreach (WaypointController waypoint in room.Waypoints.ListOfWaypoints)
                    {
                        if (waypoint.Type == WaypointType.Origin)
                        {
                            transform.position = new Vector3(waypoint.transform.position.x, waypoint.transform.position.y, transform.position.z);
                            break;
                        }
                    }

                    Debug.Log($"<color=#000080ff>{Name}</color> moved from room <color=#ffff00>{Room}</color> to <color=#ffff00>{room}</color>.");
                    Room = room;
                    break;
                }
            }

            if (isRoomValid == false)
            {
                Debug.Log($"The room {newRoom} is not valid!");
            }
        }
    }

    public void Pet()
    {
        if (_timeBetweenPets <= 0)
        {
            DisplayMood(State.Happy);
            _audioController.PlaySound("Purr");

            Debug.Log($"<color=#000080ff>{Name}</color> has been <color=#ff0000>pet</color>.");

            _timeBetweenPets = Random.Range(5, 15);

            StartCoroutine(ResetPetTimer());
        }
    }

    private IEnumerator ResetPetTimer()
    {
        yield return new WaitForSeconds(_timeBetweenPets);
        _timeBetweenPets = 0;
    }

    public void UpdateState(State newState)
    {
        Debug.Log($"<color=#00ff00>{Name}</color>'s state has been changed from <color=#ffff00>{_state}</color> to <color=#ffff00>{newState}</color>.");
        _state = newState;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Toy")
        {
            if (State == State.Bored)
            {
                ToyController toy = other.GetComponent<ToyController>();

                if (toy.Owner == null)
                {
                    _timesPlayerIgnoredCat = 0;
                    State = State.Idle;
                    toy.Owner = this;
                    StartCoroutine(PlayWithToys(toy));
                }
            }
        }
    }

    private IEnumerator PlayWithToys(ToyController toy)
    {
        _isPlaying = true;
        // TODO: play playing animation
        for (int i = 0; i < toy.TimeToPlay; i++)
        {
            Nourishment -= toy.TimeToPlay;
            yield return new WaitForSeconds(1);
        }

        if (Nourishment > 49)
        {
            State = State.Relaxing;
        }
        else
        {
            State = State.Hungry;
        }
        toy.Owner = null;
    }
}
