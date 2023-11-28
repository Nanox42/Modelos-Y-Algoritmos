using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializerPlayer : MonoBehaviour
{
    //[SerializeField]  private GameObject activePlayer;
    //[SerializeField]  private int initialPlayer=1;
    //[SerializeField]  private bool isTest;

    public GameObject activePlayer;
    public int initialPlayer = 1;
    public bool isTest;
    void Awake()
    {
        if(isTest)
        {
            ChoosePlayer.currentPlayer = initialPlayer;
        }
        
        BuilderPlayer director = new BuilderPlayer();

        //Debería ser así

        //IPlayerBuilder builder =;
        //Builder.DefineEars(ChoosePlayer.currentPlayer);
        //Builder.DefineName(ChoosePlayer.currentPlayer==1?"rabbit":"human");
        //Builder.DefineModel(ChoosePlayer.currentPlayer ==1?Resources.Load);

        //Builder.GetPlayer

        if (ChoosePlayer.currentPlayer == 1)
        {
            IPlayerBuilder humanBuilder = new HumanBuilder();
            //humanBuilder.DefineShoesColor(shoesColor); //builder va asignando
            //humanBuilder.DefineTshirtColor(tShirtColor);

            Players human = director.Builder(humanBuilder);
            SkinnedMeshRenderer[] renderer = human.modelPlayer.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer rend in renderer)
            {
                Material[] materials = rend.materials;
                materials[0].color = human.color;
            }
            activePlayer = human.modelPlayer;
        }
        else if (ChoosePlayer.currentPlayer == 2)
        {
            IPlayerBuilder rabbitBuilder = new RabbitBuilder();
            Players rabbit = director.Builder(rabbitBuilder);
            SkinnedMeshRenderer[] render = rabbit.modelPlayer.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer rend in render)
            {
                Material[] materials = rend.materials;
                materials[0].color = rabbit.color;
            }
            activePlayer = rabbit.modelPlayer;
        }
    }
}
