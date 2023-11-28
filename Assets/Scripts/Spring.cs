using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float potency;
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.Get().movement.y = potency;
            StartCoroutine("Bounce");
        }
    }
    public IEnumerator Bounce()
    {
        anim.SetBool("Active", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Active", false);
    }
}
