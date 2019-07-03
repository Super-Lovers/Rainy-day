using UnityEngine;

public class SustanenceController : MonoBehaviour
{
    [SerializeField]
    private int _nourishmentPoints;
    [SerializeField]
    private int _nourishmentDecayDelay;
    [SerializeField]
    private int _timeToDevour;
    [SerializeField]
    private Sprite _icon;

    #region Components
    AudioController _audioController;
    #endregion
}
