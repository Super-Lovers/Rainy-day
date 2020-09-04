using System;
using UnityEngine;

public class MealController : MonoBehaviour
{
    public string Name;
    [Header("(in seconds)")]
    public int TimeToCook;
    [Header("(in seconds)")]
    public int TimeToDevour;

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
