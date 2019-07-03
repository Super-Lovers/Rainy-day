using UnityEngine;
using System.Collections.Generic;

public class Cats : MonoBehaviour
{
    public static Cats Instance;
    public List<Cat> ListOfCats = new List<Cat>();

    private void Start()
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
