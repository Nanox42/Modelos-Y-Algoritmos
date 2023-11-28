
using UnityEngine;

public class RabbitBuilder : IPlayerBuilder
{
    private Players _players=new Players();

    public void DefineColors()
    {
        _players.color = Color.green;
    }

    public void DefineEars()
    {
        _players.ears = true;
    }

    public void DefineModel()
    {
        _players.modelPlayer = GameObject.Instantiate(Resources.Load<GameObject>("RabbitPlayer"));
    }

    public void DefineName()
    {
        _players.name = "Ezequielito";
    }

    public Players GetPlayer()
    {
        return _players;
    }
}
