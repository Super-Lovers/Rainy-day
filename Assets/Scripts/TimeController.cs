using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI clock_text = default;

    [Header("Environmental elements")]
    [SerializeField]
    private GameObject background_elements = default;

    private float timer = 0;

    // Clock parameters
    private int hour = 17;
    private int minutes = 20;
    private bool is_game_over = false;

    private float minute_increment = 0.30f;
    private float current_minute_increment = 0;

    // Background elements references
    private Color current_bg_color;
    private float color_decrement = 0.001f;

    private void Start() {
        current_bg_color = new Color(1, 1, 1, 1);
    }

    private void Update() {
        if (is_game_over == false) {
            if (background_elements != null) {
                if (timer >= 1) {
                    if (current_minute_increment == 0.60f) {
                        minutes++;

                        if (minutes == 60) {
                            hour++;
                            minutes = 0;
                        }

                        var minutesStr = minutes < 10 ? "0" + minutes : minutes.ToString();
                        if (clock_text != null) {
                            clock_text.text = string.Format("{0}:{1}", hour, minutesStr);
                            if (clock_text.text == "20:30") {
                                StartCoroutine(Rooms.Instance.DarkenCoroutine());
                                Invoke("GoToEndScene", 1);

                                is_game_over = true;
                            }
                        }

                        current_minute_increment = 0;

                        if (current_bg_color.r > 0.2f) {

                            DarkenBackgroundElements();
                        }
                    }

                    current_minute_increment += minute_increment;

                    timer = 0;
                }

                timer += Time.deltaTime;
            }
        }
    }

    private void GoToEndScene() {
        SceneManager.LoadScene("End Scene");
    }

    private void DarkenBackgroundElements() {
        current_bg_color = new Color(
                current_bg_color.r - color_decrement,
                current_bg_color.g - color_decrement,
                current_bg_color.b - color_decrement, 255);

        for (var i = 0; i < background_elements.transform.childCount; i++) {
            var element_shader = background_elements.transform.GetChild(i).GetComponent<Renderer>();
            element_shader.material.SetColor("_Color", current_bg_color);
        }
    }
}
