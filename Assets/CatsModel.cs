using System.Collections.Generic;
using UnityEngine;

public class CatsModel : MonoBehaviour
{
    public List<Cat> cats = new List<Cat>();
    private AudioController audioController;

    private void Start() {
        audioController = GetComponent<AudioController>();
    }

    public void ToggleCatEatingSounds(bool toggle) {
        if (toggle == true) {
            audioController.PlayLoopedSound("Cats Eating");
        } else {
            int number_of_cats_eating = 0;
            for (int i = 0; i < cats.Count; i++) {
                if (cats[i].isEating) { number_of_cats_eating++; }
            }

            if (number_of_cats_eating == 1) {
                audioController.Stop();
            }
        }
    }
}
