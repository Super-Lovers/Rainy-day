using System.Collections.Generic;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    public bool IsItCurrentlyCooking;
    public MealController MealPrepared;
    public List<MealController> Meals = new List<MealController>();

    public void Cook(MealController meal)
    {
        MealPrepared = meal;
    }
}
