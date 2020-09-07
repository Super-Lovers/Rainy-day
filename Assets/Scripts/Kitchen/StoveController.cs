using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    public bool is_cooking;
    public MealController meal_prepared;

    [Header("Bowl distribution references")]
    public List<BowlController> bowls = new List<BowlController>();

    [Header("Phases of meal being prepared")]
    public GameObject effects;
    public GameObject pan_cooking;
    public GameObject pan_finished_cooking;

    [Space(10)]
    public GameObject food_panel;

    public void Cook(MealController meal)
    {
        StartCoroutine(CompleteCooking(meal));
        food_panel.SetActive(false);
    }

    private IEnumerator CompleteCooking(MealController meal)
    {
        pan_cooking.SetActive(true);
        effects.SetActive(true);
        is_cooking = true;

        yield return new WaitForSeconds(meal.time_to_cook);

        effects.SetActive(false);
        pan_cooking.SetActive(false);
        is_cooking = false;

        meal_prepared = meal;

        if (PlayerController.Instance.debug_mode == true) {
            Debug.Log($"The meal <color=#a52a2aff>{meal.name}</color> is done cooking!");
        }
        pan_finished_cooking.SetActive(true);

        UpdateMeals();
    }

    public void UpdateMeals()
    {
        for (var i = 0; i < bowls.Count; i++) {
            var bowl = bowls[i];

            bowl.UpdateMeal(meal_prepared);
        }
    }
}
