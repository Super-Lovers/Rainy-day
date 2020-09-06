﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    [Header("The scene to load's name when any key is pressed")]
    [SerializeField]
    private string sceneToLoadOnPress;

    [SerializeField]
    private GameObject pauseMenu;

    public void ToggleWindow(Object window) {
        GameObject obj = (GameObject)window;
        obj.SetActive(!obj.activeSelf);
        AudioManager.Instance.AudioController.PlaySound("Press");
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name == "Start Scene") {
            if (Input.GetKeyUp(KeyCode.Space)) {
                SceneManager.LoadScene(sceneToLoadOnPress);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (pauseMenu != null) {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
        }
    }
}
