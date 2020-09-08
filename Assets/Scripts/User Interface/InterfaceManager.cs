using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    [Header("The scene to load's name when any key is pressed")]
    [SerializeField]
    private string scene_to_load_on_press = default;

    [SerializeField]
    private GameObject pause_menu = default;

    [SerializeField]
    private List<GameObject> interfaces = new List<GameObject>();

    private void Start() {
        if (SceneManager.GetActiveScene().name == "End Scene") {
            StartCoroutine(Rooms.Instance.LightenCoroutine());
        }
    }

    public void ToggleWindow(Object window) {
        var obj = (GameObject)window;
        obj.SetActive(!obj.activeSelf);
        AudioManager.Instance.audio_controller.PlaySound("Press");
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name == "Start Scene" ||
            SceneManager.GetActiveScene().name == "End Scene") {
            if (Input.GetKeyUp(KeyCode.Space)) {
                SceneManager.LoadScene(scene_to_load_on_press);
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (pause_menu != null) {
                if (pause_menu.activeSelf == false) {
                    for (int i = 0; i < interfaces.Count; i++) {
                        interfaces[i].SetActive(false);
                    }
                }

                pause_menu.SetActive(!pause_menu.activeSelf);
            }
        }
    }
}
