using UnityEngine;

public class BowlController : MonoBehaviour
{
    public Cat catOwner;
    public MealController meal;
    public RoomController room;
    public GameObject meal_object;

    public void UpdateMeal(MealController meal)
    {
        this.meal = meal;
        meal_object.SetActive(true);
    }
    
    public void EatSustanence() {
        meal_object.SetActive(false);
    }
}
