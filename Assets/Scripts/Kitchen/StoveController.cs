using System.Collections;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    public bool IsItCurrentlyCooking;
    public MealController MealPrepared;

    public void Cook(MealController meal)
    {
        StartCoroutine(CompleteCooking(meal));
    }

    private IEnumerator CompleteCooking(MealController meal) {
        IsItCurrentlyCooking = true;
        yield return new WaitForSeconds(meal.TimeToCook);
        IsItCurrentlyCooking = false;
        MealPrepared = meal;

        Debug.Log($"The meal <color=#a52a2aff>{meal.Name}</color> is done cooking!");
    }
}
