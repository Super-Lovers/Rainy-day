using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour, ICat
{
    public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Nourishment { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int Satisfaction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public State State { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public RoomController Room { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public BowlController Bowl { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public ToyController Toy { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    #region Components
    AudioController _audioController;
    #endregion

    private void Start()
    {
        List<Cat> listOfCats = Cats.Instance.ListOfCats;
        if (listOfCats.Contains(this) == false)
        {
            listOfCats.Add(this);
        }
    }

    private void Update()
    {
        // TODO: Coroutines of the core game loop
        // TODO: When the cat finishes eating a substance, play a sound effect.
    }

    public void ChangeRoom()
    {
        throw new System.NotImplementedException();
    }

    public void Pet()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
