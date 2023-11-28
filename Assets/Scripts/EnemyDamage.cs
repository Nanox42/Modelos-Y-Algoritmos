using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour, IDamageable
{
    public Animator anim;
    public float enemyLife = 1;
    public float deadLength = 2;
    public GameObject particles;
    public void Damage()
    {
        StartCoroutine("Dying");
    }
    public IEnumerator Dying()
    {
        //Debug.Log("Coroutine Dying");
        //anim.SetBool("dead", true);
        yield return new WaitForSeconds(deadLength);
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(transform.parent.gameObject);
    }
}
