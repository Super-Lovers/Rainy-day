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

    private float time;

    private void Start() {
        state = new CatEating();
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
