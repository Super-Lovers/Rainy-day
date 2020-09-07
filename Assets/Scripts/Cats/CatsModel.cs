using System.Collections.Generic;
using UnityEngine;

public class CatsModel : MonoBehaviour
{
    public List<Cat> cats = new List<Cat>();
    public AudioController cats_eating_audio_controller;
    public AudioController cats_playing_audio_controller;

    public void ToggleCatEatingSounds(bool toggle) {
        if (toggle == true) {
            cats_eating_audio_controller.PlayLoopedSound("Cats Eating");
        } else {
            var number_of_cats_eating = 0;
            for (var i = 0; i < cats.Count; i++) {
                if (cats[i].is_eating) { number_of_cats_eating++; }
            }

            if (number_of_cats_eating == 1) {
                cats_eating_audio_controller.Stop();
            }
        }
    }

    public void ToggleCatPlayingSounds(bool toggle) {
        if (toggle == true) {
            cats_playing_audio_controller.PlayLoopedSound("Cats Playing");
        } else {
            var number_of_cats_playing = 0;
            for (var i = 0; i < cats.Count; i++) {
                if (cats[i].is_playing) { number_of_cats_playing++; }
            }

            if (number_of_cats_playing == 1) {
                cats_playing_audio_controller.Stop();
            }
        }
    }
}
