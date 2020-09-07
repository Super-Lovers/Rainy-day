using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public static Rooms Instance;
    [NonSerialized]
    public List<RoomController> rooms = new List<RoomController>();

    [Header("Field for fade effects controller when switching rooms.")]
    public Animator transition_animator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartRoomTransition() {
        StartCoroutine(StartRoomTransitionCoroutine());
    }

    private IEnumerator StartRoomTransitionCoroutine() {
        transition_animator.SetBool("Display", true);
        yield return new WaitForSeconds(1f);
        transition_animator.SetBool("Display", false);
    }

    public IEnumerator DarkenCoroutine() {
        transition_animator.SetBool("Display", true);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator LightenCoroutine() {
        transition_animator.Play("FadeOutTransition", -1, 0f);
        yield return new WaitForEndOfFrame();
    }
}
