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

    private Animator animator_component;
    private AnimatorController animator;
    private Dictionary<string, string>
        animations = new Dictionary<string, string>();

    private NotificationDisplay notification_display;

    private float time;

    private void Start() {
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
    }

    private void Update() {
        if (time >= 1) {
            state.Cycle(this);
            TrackState();

            time = 0;
        }

        time += Time.deltaTime;
    }

    /// <summary>
    /// Responsible for updating the current state based on specific rules/conditions
    /// </summary>
    private void TrackState() {
        if (this.fatigue <= 60 &&
            this.hunger < 60) {
            if (this.happiness >= 50) {
                if (Random.Range(0, 100) > 50) {
                    state = new CatWalking(animator, animations["Walking"]);
                } else {
                    state = new CatStanding(animator, animations["Standing"]);
                }

                notification_display.SendNotification(Mood.Happy);
            } else {
                state = new CatStanding(animator, animations["Standing"]);
                notification_display.RemoveNotification(Mood.Happy);
            }

            this.emotionality = "Relaxing";

            notification_display.RemoveNotification(Mood.Tired);
            notification_display.RemoveNotification(Mood.Hungry);
        } else if (this.fatigue > 60 && this.hunger < 60) {
            state = new CatStanding(animator, animations["Standing"]);
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

    //if (transform.position.x < obj.transform.position.x)
    //{
    //    spriteRenderer.flipX = true;
    //}
    //else if (transform.position.x > obj.transform.position.x)
    //{
    //    spriteRenderer.flipX = false;
    //}
    //transform.position = Vector2.MoveTowards(transform.position, obj.transform.position, (100 * Time.deltaTime) * 0.015f);
}
