using UnityEngine;
using UnityEngine.UI;

public class CatDisplay : MonoBehaviour
{
    public Cat CatOwner;
    public Slider HappinessSlider;
    public Slider NourishmentSlider;

    public void UpdateSliders() {
        //HappinessSlider.value = CatOwner.Satisfaction * 0.01f;
        //NourishmentSlider.value = CatOwner.Nourishment * 0.01f;
    }
}
