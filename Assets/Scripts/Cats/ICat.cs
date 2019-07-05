public interface ICat
{
    string Name { get; set; }
    int Nourishment { get; set; }
    int Satisfaction { get; set; }
    State State { get; set; }
    RoomController Room { get; set; }
    BowlController Bowl { get; set; }
    ToyController Toy { get; set; }

    void ChangeRoom(string newRoom);
    WaypointController GetNewWaypoint(WaypointType newWaypointType);
    void UpdateState(State newState);
    void Pet();
    void Eat();
}