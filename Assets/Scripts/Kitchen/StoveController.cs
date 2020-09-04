using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveController : MonoBehaviour
{
    public bool IsItCurrentlyCooking;
    public MealController MealPrepared;

    [Header("Bowl distribution references")]
    public List<BowlController> Bowls = new List<BowlController>();

    [Header("Phases of meal being prepared")]
    public GameObject Effects;
    public GameObject PanCooking;
    public GameObject PanFinishedCooking;

    [Space(10)]
    public GameObject foodPanel;

    public void Cook(MealController meal)
    {
        StartCoroutine(CompleteCooking(meal));
        foodPanel.SetActive(false);
    }

    private IEnumerator CompleteCooking(MealController meal)
    {
        PanCooking.SetActive(true);
        Effects.SetActive(true);
        IsItCurrentlyCooking = true;

        yield return new WaitForSeconds(meal.TimeToCook);

        Effects.SetActive(false);
        PanCooking.SetActive(false);
        IsItCurrentlyCooking = false;

        MealPrepared = meal;

        Debug.Log($"The meal <color=#a52a2aff>{meal.Name}</color> is done cooking!");
        PanFinishedCooking.SetActive(true);

        UpdateMeals();
    }

    public void UpdateMeals()
    {
        for (int i = 0; i < Bowls.Count; i++) {
            var bowl = Bowls[i];

            bowl.UpdateMeal(MealPrepared);
        }
    }
}
