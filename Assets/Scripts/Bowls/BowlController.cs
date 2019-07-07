using UnityEngine;

public class BowlController : MonoBehaviour
{
    public Cat CatOwner;
    public SustanenceController Sustenance;
    public RoomController Room;
    public GameObject MealObject;

    public void UpdateSustanence(SustanenceController sustenance)
    {
        Sustenance = sustenance;
    }
    
    public void EatSustanence() {
        CatOwner.State = State.Relaxing;
        MealObject.SetActive(false);
        Destroy(Sustenance.gameObject);
    }
}
