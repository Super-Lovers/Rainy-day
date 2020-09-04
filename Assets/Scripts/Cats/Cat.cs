﻿using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    protected string name;

    protected CatState state;
    [SerializeField]
    protected string state_name;
    [SerializeField]
    protected string emotionality;

    [Space(10)]
    [SerializeField]
    protected float happiness;
    public float Happiness {
        get { return this.happiness; }
        set { this.happiness = value; }
    }

    [SerializeField]
    protected float hunger;
    public float Hunger {
        get { return this.hunger; }
        set { this.hunger = value; }
    }

    [SerializeField]
    protected float fatigue;
    public float Fatigue {
        get { return this.fatigue; }
        set { this.fatigue = value; }
    }

    [Space(10)]
    [SerializeField]
    protected RoomController room;
    [SerializeField]
    protected BowlController bowl;
    [SerializeField]
    protected ToyController toy;

    [SerializeField]
    protected WaypointController waypoint;

    // Object components
    private SpriteRenderer spriteRenderer;
    private Animator animator_component;
    private RuntimeAnimatorController animator;
    public AudioController audioController;
    private NotificationDisplay notification_display;

    // Animations
    private Dictionary<string, string>
        animations = new Dictionary<string, string>();

    // Moving and standing variables
    private float time;
    private float timeUntilNextAction;
    private bool enteringNewRoom;

    // Petting mechanic variables
    private const float DEFAULT_CHANCE_TO_PET = 5;
    private float chance_to_pet = 5; // 0 to 100%
    private float time_until_next_pet;

    // Meals references
    private bool isEating = false;

    private void Start() {
        enteringNewRoom = true;

        audioController = GetComponent<AudioController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        notification_display = GetComponentInChildren<NotificationDisplay>();
        notification_display.gameObject.SetActive(false);

        animator_component = GetComponentInChildren<Animator>();

        animator = Resources.Load<RuntimeAnimatorController>(
            $"Animations/Cats/{this.name}/{this.name + "CatAnimator"}");
        animator_component.runtimeAnimatorController = animator;

        animations.Add("Walking", $"{this.name + "WalkAnimation"}");
        animations.Add("Eating", $"{this.name + "EatAnimation"}");
        animations.Add("Standing", $"{this.name + "IdleAnimation"}");
        animations.Add("Sleeping", $"{this.name + "WalkAnimation"}");

        state = new CatWalking(animator, animations["Walking"]);

        waypoint = GetRandomWaypoint(false);
        //EnterRoom("Kitchen");
    }

    private void Update() {
        TrackState();

        if (time >= 1) {
            state.Cycle(this);

            time = 0;
        }

        if (time_until_next_pet > 0) {
            time_until_next_pet -= Time.deltaTime;
        }

        time += Time.deltaTime;

        if (this.emotionality == "Hungry" && bowl.MealObject.activeSelf == true && isEating == false) {
            if (room.Name != "Kitchen") {
                EnterRoom("Kitchen");
                Walk();

                if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.2f) {
                    if (waypoint.Type == WaypointType.Origin) {
                        ChangeToRoom(waypoint.newRoom);
                    }
                }

                return;
            }

            if (Vector2.Distance(transform.position, bowl.transform.position) < 0.1f) {
                state = new CatEating(animator, animations["Eating"]);
                Invoke("StopEating", bowl.Meal.TimeToDevour);

                isEating = true;
            }

            WalkTo(bowl.transform);
            return;
        }

        if (isEating) { return; }

        if (enteringNewRoom == false) {
            if (timeUntilNextAction <= 0) {
                timeUntilNextAction = UnityEngine.Random.Range(3, 8);
                waypoint = GetRandomWaypoint(false);
            } else {
                timeUntilNextAction -= Time.deltaTime;
            }
        }

        if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.2f) {
            state = new CatStanding(animator, animations["Standing"]);

            if (enteringNewRoom == true) {
                enteringNewRoom = false;
            }

            if (waypoint.Type == WaypointType.Origin) {
                ChangeToRoom(waypoint.newRoom);
            }
        } else {
            state = new CatWalking(animator, animations["Walking"]);

            Walk();
        }
    }

    private void WalkTo(Transform obj) {
        if (transform.position.x < obj.transform.position.x) {
            spriteRenderer.flipX = true;
        } else if (transform.position.x > obj.transform.position.x) {
            spriteRenderer.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, obj.transform.position, (100 * Time.deltaTime) * 0.015f);
    }

    private void Walk() {
        if (transform.position.x < waypoint.transform.position.x) {
            spriteRenderer.flipX = true;
        } else if (transform.position.x > waypoint.transform.position.x) {
            spriteRenderer.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, (100 * Time.deltaTime) * 0.015f);
    }

    private void ChangeToRoom(RoomController room) {
        this.room = room;
        waypoint = GetRandomWaypoint(true);

        gameObject.transform.position = new Vector3(
            waypoint.transform.position.x,
            waypoint.transform.position.y,
            transform.position.z);
    }

    /// <summary>
    /// Responsible for updating the current state based on specific rules/conditions
    /// </summary>
    private void TrackState() {
        if (this.fatigue <= 60 &&
            this.hunger < 60) {
            if (this.happiness >= 50) {
                if (Random.Range(0, 100) > 50) {
                    //state = new CatWalking(animator, animations["Walking"]);
                } else {
                    //state = new CatStanding(animator, animations["Standing"]);
                }

                notification_display.SendNotification(Mood.Happy);
            } else {
                //state = new CatStanding(animator, animations["Standing"]);
                StartCoroutine(notification_display.RemoveNotification(Mood.Happy, 0));
            }

            this.emotionality = "Relaxing";

            StartCoroutine(notification_display.RemoveNotification(Mood.Tired, 0));
            StartCoroutine(notification_display.RemoveNotification(Mood.Hungry, 0));
        } else if (this.fatigue > 60 && this.hunger < 60) {
            this.emotionality = "Tired";

            StartCoroutine(notification_display.RemoveNotification(Mood.Hungry, 0));
            notification_display.SendNotification(Mood.Tired);
            StartCoroutine(notification_display.RemoveNotification(Mood.Happy, 0));
        } else if (this.fatigue > 60 && this.hunger >= 60) {
            this.emotionality = "Hungry and Tired";
            notification_display.SendNotification(Mood.Tired);
            notification_display.SendNotification(Mood.Hungry);
            StartCoroutine(notification_display.RemoveNotification(Mood.Happy, 0));
        } else if (this.fatigue <= 60 && this.hunger >= 60) {
            this.emotionality = "Hungry";
            notification_display.SendNotification(Mood.Hungry);
            StartCoroutine(notification_display.RemoveNotification(Mood.Tired, 0));
            StartCoroutine(notification_display.RemoveNotification(Mood.Happy, 0));
        }

        animator_component.Play(state.Animation);
        state_name = state.GetType().ToString();
    }

    private void EnterRoom(string name) {
        for (int i = 0; i < room.Waypoints.ListOfWaypoints.Count; i++) {
            var newWaypoint = room.Waypoints.ListOfWaypoints[i];
            if (newWaypoint.newRoom.Name == name) {
                waypoint = newWaypoint;
                break;
            }
        }
    }

    private WaypointController GetRandomWaypoint(bool ignoreOrigin) {
        if (waypoint != null) {
            waypoint.isWaypointOccupied = false;
        }
        var availableWaypoints = new List<WaypointController>();

        for (int i = 0; i < room.Waypoints.ListOfWaypoints.Count; i++) {
            var waypoint = room.Waypoints.ListOfWaypoints[i];

            if (ignoreOrigin == true) {
                if (waypoint.Type != WaypointType.Origin) {
                    if (waypoint.isWaypointOccupied == false) {
                        availableWaypoints.Add(waypoint);
                    }
                }
            } else {
                if (waypoint.isWaypointOccupied == false) {
                    availableWaypoints.Add(waypoint);
                }
            }
        }

        var newWaypoint = availableWaypoints[
                   Random.Range(0, availableWaypoints.Count)];

        newWaypoint.isWaypointOccupied = true;

        return newWaypoint;
    }

    /// <summary>
    /// Whenever the user taps on the cat, there will be a chance to increase
    /// the pet's happiness. This function contains the mechanic for that.
    /// </summary>
    public void Pet() {
        if (time_until_next_pet <= 0) {
            var chance = Random.Range(0, 101);

            if (chance <= chance_to_pet) {
                Happiness += Random.Range(7, 10);
                chance_to_pet = DEFAULT_CHANCE_TO_PET;
                notification_display.SendNotification(Mood.Purry);
            } else {
                chance_to_pet += Random.Range(4, 8);
            }

            time_until_next_pet = Random.Range(2, 8);
        }
    }

    private void StopEating() {
        state = new CatStanding(animator, animations["Standing"]);
        bowl.EatSustanence();
        isEating = false;
    }
}
