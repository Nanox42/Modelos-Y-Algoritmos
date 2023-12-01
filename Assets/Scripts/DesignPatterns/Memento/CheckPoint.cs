using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //private IMemento _player;
    //private IMemento _coin;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            //_player = ((IOriginator)PlayerController.Get()).Save();
            //_coin = ((IOriginator)PlayerController.Get()).Save();

            PlayerController.Get().SetPosition();
            //((IOriginator)PlayerController.Get()).Save();
            PlayerController.Get().SaveCheckPoint();
        }
    }
}
