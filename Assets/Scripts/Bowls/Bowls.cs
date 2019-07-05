using System.Collections.Generic;
using UnityEngine;

public class Bowls : MonoBehaviour
{
    public List<BowlController> ListOfBowls = new List<BowlController>();
    // These bowls will be the ones selected once a meal is ready and the player must
    // pick from his cats' bowls which he wants to fill up.
    public List<BowlController> ListOfSelectedBowls = new List<BowlController>();
    public StoveController Stove;

    public void UpdateSustanenceOfBowl(BowlController bowl)
    {
        bowl.UpdateSustanence(Stove.MealPrepared.Sustanence);
    }
}
