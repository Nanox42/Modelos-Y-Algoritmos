
using UnityEngine;

public class HumanBuilder : IPlayerBuilder
{
    private Players _players=new Players();

    

    public void DefineColors()
    {
        _players.color = Color.blue;
    }

    public void DefineEars()
    {
        _players.ears = false;
    }

    public void DefineModel()
    {
        _players.modelPlayer = GameObject.Instantiate(Resources.Load<GameObject>("HumanPlayer"));
    }

    public void DefineName()
    {
        _players.name = "Juancito";
    }

    void Start()
    {
        DefineName("Juancito");
    }

    public void DefineName(string name)
    {
        _players.name = name;
    }

    public Players GetPlayer()
    {
        return _players;
    }
}
