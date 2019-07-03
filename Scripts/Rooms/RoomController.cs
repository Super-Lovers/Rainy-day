using UnityEngine;
using System;

public class RoomController : MonoBehaviour
{
    [NonSerialized]
    public Sprite Background;
    public Waypoints Waypoints;

    private void Awake()
    {
        Background = GetComponentsInChildren<SpriteRenderer>()[0].sprite;
    }
}
