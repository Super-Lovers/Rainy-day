using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveController : MonoBehaviour
{
    public bool IsItCurrentlyCooking;
    public MealController MealPrepared;

    [Header("Bowl distribution references")]
    public GameObject BowlSelector;
    public List<GameObject> Bowls = new List<GameObject>();

    [Header("Phases of meal being prepared")]
    public GameObject Effects;
    public GameObject PanCooking;
    public GameObject PanFinishedCooking;

    public void Cook(MealController meal)
    {
        StartCoroutine(CompleteCooking(meal));
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

        BowlSelector.SetActive(true);
        Debug.Log($"The meal <color=#a52a2aff>{meal.Name}</color> is done cooking!");
        PanFinishedCooking.SetActive(true);
    }

    public void UpdateSustanence(BowlController bowl)
    {
        if (MealPrepared.Quantity > 0 && bowl.Sustenance == null)
        {
            bowl.UpdateSustanence(MealPrepared.Sustanence);
            bowl.MealObject.SetActive(true);

            MealPrepared.Quantity--;
            Debug.Log($"Sustanence <color=#a52a2aff>{MealPrepared.Sustanence.Name}</color> is placed in {bowl.CatOwner.Name}'s bowl.");
        }

        if (MealPrepared.Quantity <= 0)
        {
            foreach (GameObject obj in Bowls)
            {
                Image imageComponent = obj.GetComponent<Image>();
                imageComponent.color = new Color(255, 255, 255, 1);
                imageComponent.raycastTarget = true;
            }

            PanFinishedCooking.SetActive(false);
            BowlSelector.SetActive(false);
        }
    }

    public void DisableBowlIcon(Image bowlToDisable)
    {
        bowlToDisable.color = new Color(255, 255, 255, 0.3f);
        bowlToDisable.raycastTarget = false;
    }
}
