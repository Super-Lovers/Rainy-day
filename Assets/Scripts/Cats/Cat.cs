using System;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    private SpriteRenderer spriteRenderer;
    private Animator animator_component;
    private AnimatorController animator;
    private Dictionary<string, string>
        animations = new Dictionary<string, string>();

    private NotificationDisplay notification_display;

    private float time;
    private float timeUntilNextAction;

    private void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        notification_display = GetComponentInChildren<NotificationDisplay>();
        notification_display.gameObject.SetActive(false);

        animator_component = GetComponentInChildren<Animator>();

        animator = Resources.Load<AnimatorController>(
            $"Animations/Cats/{this.name}/{this.name + "CatAnimator"}");
        animator_component.runtimeAnimatorController = animator;

        animations.Add("Walking", $"{this.name + "WalkAnimation"}");
        animations.Add("Eating", $"{this.name + "EatAnimation"}");
        animations.Add("Standing", $"{this.name + "IdleAnimation"}");
        animations.Add("Sleeping", $"{this.name + "WalkAnimation"}");

        state = new CatWalking(animator, animations["Walking"]);

        waypoint = GetRandomWaypoint();
    }

    private void Update() {
        if (time >= 1) {
            state.Cycle(this);
            TrackState();

            time = 0;
        }

        if (timeUntilNextAction <= 0) {
            timeUntilNextAction = UnityEngine.Random.Range(3, 8);
            waypoint = GetRandomWaypoint();
        } else {
            timeUntilNextAction -= Time.deltaTime;
        }

        time += Time.deltaTime;

        if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.1f) {
            state = new CatStanding(animator, animations["Standing"]);
        } else {
            state = new CatWalking(animator, animations["Walking"]);

            if (transform.position.x < waypoint.transform.position.x) {
                spriteRenderer.flipX = true;
            } else if (transform.position.x > waypoint.transform.position.x) {
                spriteRenderer.flipX = false;
            }

            transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, (100 * Time.deltaTime) * 0.015f);
        }
    }

    /// <summary>
    /// Responsible for updating the current state based on specific rules/conditions
    /// </summary>
    private void TrackState() {
        if (this.fatigue <= 60 &&
            this.hunger < 60) {
            if (this.happiness >= 50) {
                if (UnityEngine.Random.Range(0, 100) > 50) {
                    //state = new CatWalking(animator, animations["Walking"]);
                } else {
                    //state = new CatStanding(animator, animations["Standing"]);
                }

                notification_display.SendNotification(Mood.Happy);
            } else {
                //state = new CatStanding(animator, animations["Standing"]);
                notification_display.RemoveNotification(Mood.Happy);
            }

            this.emotionality = "Relaxing";

            notification_display.RemoveNotification(Mood.Tired);
            notification_display.RemoveNotification(Mood.Hungry);
        } else if (this.fatigue > 60 && this.hunger < 60) {
            this.emotionality = "Tired";

            notification_display.RemoveNotification(Mood.Hungry);
            notification_display.SendNotification(Mood.Tired);
            notification_display.RemoveNotification(Mood.Happy);
        } else if (this.fatigue > 60 && this.hunger >= 60) {
            this.emotionality = "Hungry and Tired";
            notification_display.SendNotification(Mood.Tired);
            notification_display.SendNotification(Mood.Hungry);
            notification_display.RemoveNotification(Mood.Happy);
        } else if (this.fatigue <= 60 && this.hunger >= 60) {
            this.emotionality = "Hungry";
            notification_display.SendNotification(Mood.Hungry);
            notification_display.RemoveNotification(Mood.Tired);
            notification_display.RemoveNotification(Mood.Happy);
        }

        animator_component.Play(state.Animation);
        state_name = state.GetType().ToString();
    }

    private WaypointController GetRandomWaypoint() {
        if (waypoint != null) {
            waypoint.IsWaypointOccupied = false;
        }
        var availableWaypoints = new List<WaypointController>();

        for (int i = 0; i < room.Waypoints.ListOfWaypoints.Count; i++) {
            var waypoint = room.Waypoints.ListOfWaypoints[i];

            if (waypoint.IsWaypointOccupied == false) {
                availableWaypoints.Add(waypoint);
            }
        }

        var newWaypoint = availableWaypoints[
                   UnityEngine.Random.Range(0, availableWaypoints.Count)];

        newWaypoint.IsWaypointOccupied = true;

        return newWaypoint;
    }
}
