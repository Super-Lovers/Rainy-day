using UnityEngine;

public class SustanenceController : MonoBehaviour, ISustanence
{
    [SerializeField]
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    [SerializeField]
    private int _nourishmentPoints;
    public int NourishmentPoints
    {
        get
        {
            return _nourishmentPoints;
        }
        set
        {
            _nourishmentPoints = value;
        }
    }
    [SerializeField]
    private int _nourishmentDecayDelay;
    public int NourishmentDecayDelay
    {
        get
        {
            return _nourishmentDecayDelay;
        }
        set
        {
            _nourishmentDecayDelay = value;
        }
    }
    [SerializeField]
    private int _timeToDevour;
    public int TimeToDevour
    {
        get
        {
            return _timeToDevour;
        }
        set
        {
            _timeToDevour = value;
        }
    }

    #region Components
    AudioController _audioController;
    #endregion
}
