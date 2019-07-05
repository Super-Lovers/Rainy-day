using UnityEngine;

public class BowlController : MonoBehaviour
{
    public Cat CatOwner;
    public SustanenceController Sustenance;

    public void UpdateSustanence(SustanenceController sustenance)
    {
        Sustenance = sustenance;
    }
    
    public void EatSustanence() {
        Destroy(Sustenance.gameObject);
    }
}
