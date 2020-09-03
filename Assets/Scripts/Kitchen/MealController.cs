using System;
using UnityEngine;

public class MealController : MonoBehaviour
{
    public string Name;
    public int TimeToCook;
    public int TimeToDevour;
    public int Quantity;

    #region Components
    [NonSerialized]
    public StoveController stove;
    #endregion

    private void Start() {
        stove = GetComponentInParent<StoveController>();
    }

    public void OnMouseUp() {
        stove.Cook(this);
    }
}
