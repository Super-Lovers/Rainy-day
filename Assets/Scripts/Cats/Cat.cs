using UnityEngine;

public class Cat
{
    [SerializeField]
    protected string name;

    [SerializeField]
    protected CatState state;

    [SerializeField]
    protected int happiness;
    [SerializeField]
    protected int hunger;
    [SerializeField]
    protected int fatigue;

    [SerializeField]
    protected RoomController room;
    [SerializeField]
    protected BowlController bowl;
    [SerializeField]
    protected ToyController toy;

    [SerializeField]
    protected WaypointController waypoint;

    private float time;

    private void Update() {
        if (time >= 1) {
            state.Cycle(this);
            time = 0;
        }

        time += Time.deltaTime;
    }

    public void UpdateHappiness(int value) {
        this.happiness += value;
    }

    public void UpdateHunger(int value) {
        this.hunger += value;
    }

    public void UpdateFatigue(int value) {
        this.fatigue += value;
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
