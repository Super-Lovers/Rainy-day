using UnityEngine;

public class BowlController : MonoBehaviour
{
    public Sprite PortraitOfOwner;
    public SustanenceController Sustenance;

    public void UpdateSustanence(SustanenceController sustenance)
    {
        Sustenance = sustenance;
    }
    
    public void EatSustanence() {
        Destroy(Sustenance.gameObject);
    }
}
