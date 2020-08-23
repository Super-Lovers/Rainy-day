using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    protected string name;

    [Space(10)]
    [SerializeField]
    protected CatState state;

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

    private float time;

    private void Start() {
        animator_component = GetComponentInChildren<Animator>();

        animator = Resources.Load<AnimatorController>(
            $"Animations/Cats/{this.name}/{this.name + "CatAnimator"}");
        animator_component.runtimeAnimatorController = animator;

        //var walking_animation = Resources.Load<Animation>(
        //    $"Animations/Cats/{this.name}/{this.name + "WalkAnimation"}");
        //var eating_animation = Resources.Load<Animation>(
        //    $"Animations/Cats/{this.name}/{this.name + "EatingAnimation"}");
        //var standing_animation = Resources.Load<Animation>(
        //    $"Animations/Cats/{this.name}/{this.name + "IdleAnimation"}");
        //var sleeping_animation = Resources.Load<Animation>(
        //    $"Animations/Cats/{this.name}/{this.name + "SleepingAnimation"}");
        
        animations.Add("Walking", $"{this.name + "WalkAnimation"}");
        animations.Add("Eating", $"{this.name + "EatAnimation"}");
        animations.Add("Standing", $"{this.name + "IdleAnimation"}");
        animations.Add("Sleeping", $"{this.name + "WalkAnimation"}");

        state = new CatWalking(animator, animations["Walking"]);
        animator_component.Play(state.Animation);
    }

    private void Update() {
        if (time >= 1) {
            state.Cycle(this);
            time = 0;
        }

        time += Time.deltaTime;
    }

    private void TrackState() {

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
