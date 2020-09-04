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
        MealObject.SetActive(true);
    }
    
    public void EatSustanence() {
        MealObject.SetActive(false);
    }
}
