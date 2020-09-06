using System.Collections.Generic;
using UnityEngine;

public class CatsModel : MonoBehaviour
{
    public List<Cat> cats = new List<Cat>();
    public AudioController catsEating_audioController;
    public AudioController catsPlaying_audioController;

    public void ToggleCatEatingSounds(bool toggle) {
        if (toggle == true) {
            catsEating_audioController.PlayLoopedSound("Cats Eating");
        } else {
            int number_of_cats_eating = 0;
            for (int i = 0; i < cats.Count; i++) {
                if (cats[i].isEating) { number_of_cats_eating++; }
            }

            if (number_of_cats_eating == 1) {
                catsEating_audioController.Stop();
            }
        }
    }

    public void ToggleCatPlayingSounds(bool toggle) {
        if (toggle == true) {
            catsPlaying_audioController.PlayLoopedSound("Cats Playing");
        } else {
            int number_of_cats_playing = 0;
            for (int i = 0; i < cats.Count; i++) {
                if (cats[i].isPlaying) { number_of_cats_playing++; }
            }

            if (number_of_cats_playing == 1) {
                catsPlaying_audioController.Stop();
            }
        }
    }
}
