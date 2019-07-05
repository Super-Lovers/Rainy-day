using System;
using UnityEngine;

public class MealController : MonoBehaviour
{
    public string Name;
    public SustanenceController Sustanence;
    public int TimeToCook;
    public int Quantity;

    #region Components
    [NonSerialized]
    public StoveController Stove;
    #endregion

    private void Start() {
        Stove = GetComponentInParent<StoveController>();
    }
}
