using UnityEngine;
using System.Collections.Generic;
using System;

public class Cats : MonoBehaviour
{
    public static Cats Instance;
    [NonSerialized]
    public List<Cat> ListOfCats = new List<Cat>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        } else
        {
            Instance = this;
        }
    }
}
