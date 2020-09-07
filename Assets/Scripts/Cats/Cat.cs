using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    private new string name = default;

    private CatState state;
    [SerializeField]
    private string emotionality;

    [Space(10)]
    [SerializeField]
    private float happiness;
    public float Happiness {
        get { return this.happiness; }
        set { this.happiness = value; }
    }

    [SerializeField]
    private float hunger;
    public float Hunger {
        get { return this.hunger; }
        set { this.hunger = value; }
    }

    [SerializeField]
    private float fatigue;
    public float Fatigue {
        get { return this.fatigue; }
        set { this.fatigue = value; }
    }

    [Space(10)]
    [SerializeField]
    private RoomController room;
    [SerializeField]
    private BowlController bowl = default;
    [SerializeField]
    private ToyController toy = default;
    [SerializeField]
    private CatsModel model = default;

    [Space(10)]
    [SerializeField]
    private WaypointController waypoint;

    // Object components
    private SpriteRenderer sprite_renderer;
    private Animator animator_component;
    private RuntimeAnimatorController animator;
    public AudioController audio_controller;
    private NotificationDisplay notification_display;

    // Animations
    private Dictionary<string, string>
        animations = new Dictionary<string, string>();

    // Moving and standing variables
    private float time;
    private float time_until_next_action;
    private bool entering_new_room;

    // Petting mechanic variables
    private const float DEFAULT_CHANCE_TO_PET = 5;
    private float chance_to_pet = 5; // 0 to 100%
    private float time_until_next_pet;

    // Meals references
    public bool is_eating = false;

    // Playing with toy references
    public bool is_playing = false;

    // Sleeping references
    private bool is_sleeping = false;

    // Meowing references
    private float meow_delay;
    private float meow_counter = 0;

    private void Start() {
        meow_delay = Random.Range(8, 26);
        entering_new_room = true;

        audio_controller = GetComponent<AudioController>();
        sprite_renderer = GetComponentInChildren<SpriteRenderer>();
        notification_display = GetComponentInChildren<NotificationDisplay>();
        notification_display.gameObject.SetActive(false);

        animator_component = GetComponentInChildren<Animator>();

        animator = Resources.Load<RuntimeAnimatorController>(
            $"Animations/Cats/{this.name}/{this.name + "CatAnimator"}");
        animator_component.runtimeAnimatorController = animator;

        animations.Add("Walking", $"{this.name + "WalkAnimation"}");
        animations.Add("Eating", $"{this.name + "EatAnimation"}");
        animations.Add("Standing", $"{this.name + "IdleAnimation"}");
        animations.Add("Sleeping", $"{this.name + "RelaxAnimation"}");

        state = new CatWalking(animator, animations["Walking"]);

        waypoint = GetRandomWaypoint(false);

        if (model.cats.Contains(this) == false) {
            model.cats.Add(this);
        }
        //EnterRoom("Kitchen");
    }

    private void Update() {
        TrackState();

        if (meow_counter < meow_delay) {
            meow_counter += Time.deltaTime;
        } else {
            var chance = Random.Range(0, 101);
            if (chance < 33) {
                audio_controller.PlaySound("Meow 1");
            }
            if (chance >= 33 && chance < 66) {
                audio_controller.PlaySound("Meow 2");
            }
            if (chance >= 66 && chance < 100) {
                audio_controller.PlaySound("Meow 3");
            }

            meow_delay = Random.Range(8, 26);
            meow_counter = 0;
        }

        if (time >= 1) {
            state.Cycle(this);

            time = 0;
        }

        if (time_until_next_pet > 0) {
            time_until_next_pet -= Time.deltaTime;
        }

        time += Time.deltaTime;

        if ((this.emotionality == "Hungry" ||
            this.emotionality == "Hungry and Tired") &&
            bowl.meal_object.activeSelf == true &&
            is_eating == false &&
            is_sleeping == false) {
            if (room.name != "Kitchen") {
                EnterRoom("Kitchen");
                Walk();

                if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.2f) {
                    if (waypoint.type == WaypointType.Origin) {
                        ChangeToRoom(waypoint.new_room);
                    }
                }

                return;
            }

            if (Vector2.Distance(transform.position, bowl.transform.position) < 0.1f) {
                state = new CatEating(animator, animations["Eating"]);
                Invoke("StopEating", bowl.meal.time_to_devour);

                model.ToggleCatEatingSounds(true);
                is_eating = true;
            } else {
                state = new CatWalking(animator, animations["Walking"]);
            }

            WalkTo(bowl.transform);
            return;
        }

        if (is_eating) { return; }

        if (this.emotionality == "Tired" &&
            room.IsLightsOn() == false &&
            is_sleeping == false &&
            Vector2.Distance(transform.position, waypoint.transform.position) < 0.2f) {

            state = new CatSleeping(animator, animations["Sleeping"]);

            StartCoroutine(notification_display.RemoveNotification(Mood.Tired, 0));
            notification_display.SendNotification(Mood.Sleeping);

            is_sleeping = true;

            Invoke("StopSleeping", 120);
        }

        if (is_sleeping) { return; }

        if (this.emotionality != "Tired" &&
            this.emotionality != "Hungry and Tired" &&
            this.emotionality != "Hungry" &&
            this.happiness < 40) {
            if (Vector2.Distance(transform.position, toy.transform.position) < 0.5f) {
                is_playing = true;

                model.ToggleCatPlayingSounds(true);
                state = new CatPlaying(animator, animations["Standing"]);
                notification_display.SendNotification(Mood.Playing);

                Invoke("StopPlaying", toy.time_to_play);
            } else {
                if (is_playing) { StopPlaying(); }
            }
        }

        if (is_playing) { return; }

        if (entering_new_room == false) {
            if (time_until_next_action <= 0) {
                time_until_next_action = UnityEngine.Random.Range(3, 8);
                waypoint = GetRandomWaypoint(false);
            } else {
                time_until_next_action -= Time.deltaTime;
            }
        }

        if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.2f) {
            state = new CatStanding(animator, animations["Standing"]);

            if (entering_new_room == true) {
                entering_new_room = false;
            }

            if (waypoint.type == WaypointType.Origin) {
                ChangeToRoom(waypoint.new_room);
            }
        } else {
            state = new CatWalking(animator, animations["Walking"]);

            Walk();
        }
    }

    private void WalkTo(Transform obj) {
        if (transform.position.x < obj.transform.position.x) {
            sprite_renderer.flipX = true;
        } else if (transform.position.x > obj.transform.position.x) {
            sprite_renderer.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, obj.transform.position, (100 * Time.deltaTime) * 0.015f);
    }

    private void Walk() {
        if (transform.position.x < waypoint.transform.position.x) {
            sprite_renderer.flipX = true;
        } else if (transform.position.x > waypoint.transform.position.x) {
            sprite_renderer.flipX = false;
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
            if (is_sleeping == false) {
                notification_display.SendNotification(Mood.Tired);
            }
            StartCoroutine(notification_display.RemoveNotification(Mood.Happy, 0));
        } else if (this.fatigue > 60 && this.hunger >= 60) {
            this.emotionality = "Hungry and Tired";
            if (is_sleeping == false) {
                notification_display.SendNotification(Mood.Tired);
                notification_display.SendNotification(Mood.Hungry);
            }
            StartCoroutine(notification_display.RemoveNotification(Mood.Happy, 0));
        } else if (this.fatigue <= 60 && this.hunger >= 60) {
            this.emotionality = "Hungry";
            if (is_sleeping == false) {
                notification_display.SendNotification(Mood.Hungry);
            }
            StartCoroutine(notification_display.RemoveNotification(Mood.Tired, 0));
            StartCoroutine(notification_display.RemoveNotification(Mood.Happy, 0));
        }

        animator_component.Play(state.Animation);
    }

    private void EnterRoom(string name) {
        for (var i = 0; i < room.waypoints.waypoints.Count; i++) {
            var new_waypoint = room.waypoints.waypoints[i];
            if (new_waypoint.new_room.name == name) {
                waypoint = new_waypoint;
                break;
            }
        }
    }

    private WaypointController GetRandomWaypoint(bool ignore_origin) {
        if (waypoint != null) {
            waypoint.is_waypoint_occupied = false;
        }
        var availablewaypoints = new List<WaypointController>();

        for (var i = 0; i < room.waypoints.waypoints.Count; i++) {
            var current_waypoint = room.waypoints.waypoints[i];

            if (ignore_origin == true) {
                if (current_waypoint.type != WaypointType.Origin) {
                    if (current_waypoint.is_waypoint_occupied == false) {
                        availablewaypoints.Add(current_waypoint);
                    }
                }
            } else {
                if (current_waypoint.is_waypoint_occupied == false) {
                    availablewaypoints.Add(current_waypoint);
                }
            }
        }

        var new_waypoint = availablewaypoints[
                   Random.Range(0, availablewaypoints.Count)];

        new_waypoint.is_waypoint_occupied = true;

        return new_waypoint;
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

        model.ToggleCatEatingSounds(false);
        is_eating = false;
    }

    private void StopPlaying() {
        state = new CatStanding(animator, animations["Standing"]);
        StartCoroutine(notification_display.RemoveNotification(Mood.Playing, 0));

        model.ToggleCatPlayingSounds(false);
        is_playing = false;
    }

    private void StopSleeping() {
        state = new CatStanding(animator, animations["Standing"]);
        StartCoroutine(notification_display.RemoveNotification(Mood.Sleeping, 0));

        is_sleeping = false;
    }
}
