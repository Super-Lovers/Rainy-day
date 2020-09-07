using System;
using UnityEngine;

public class MealController : MonoBehaviour
{
    public new string name;
    [Header("(in seconds)")]
    public int time_to_cook;
    [Header("(in seconds)")]
    public int time_to_devour;

    [NonSerialized]
    public StoveController stove;

    private void Start() {
        stove = GetComponentInParent<StoveController>();
    }

    public void OnMouseUp() {
        stove.Cook(this);
    }
}
