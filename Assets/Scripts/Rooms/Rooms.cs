using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Rooms : MonoBehaviour
{
    public static Rooms Instance;
    [NonSerialized]
    public List<RoomController> ListOfRooms = new List<RoomController>();

    #region Transition mechanic references
    [Header("Field for fade effects controller when switching rooms.")]
    public Animator TransitionAnimator;
    #endregion

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
        TransitionAnimator.SetBool("Display", true);
        yield return new WaitForSeconds(1f);
        TransitionAnimator.SetBool("Display", false);
    }

    public IEnumerator DarkenCoroutine() {
        TransitionAnimator.SetBool("Display", true);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator LightenCoroutine() {
        TransitionAnimator.Play("FadeOutTransition", -1, 0f);
        yield return new WaitForEndOfFrame();
    }
}
