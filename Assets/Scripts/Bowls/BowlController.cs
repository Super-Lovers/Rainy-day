using UnityEngine;

public class BowlController : MonoBehaviour
{
    public Cat CatOwner;
    public MealController Meal;
    public RoomController Room;
    public GameObject MealObject;

    public void UpdateMeal(MealController meal)
    {
        Meal = meal;
    }
    
    public void EatSustanence() {
        //CatOwner.State = State.Relaxing;
        MealObject.SetActive(false);
    }
}
