using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float force = 500f;
    private Vector3 direction = Vector3.left;
    [SerializeField] private AudioClip fireBall;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float returnTime = 3.5f;
    private Pool pool;

    void OnEnable()
    {
        ResetParameters();
        Launch(direction);
        StartCoroutine(ReturnToPool(returnTime));
    }

    private void ResetParameters()
    {
        rigidbody.velocity = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    public void Launch(Vector3 direction)
    {
        audioSource.PlayOneShot(fireBall);
        rigidbody.AddForce(direction.normalized * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Impacto!");
            gameObject.SetActive(false);
        }
    }

    private IEnumerator ReturnToPool(float time)
    {
        yield return new WaitForSeconds(time);
        pool.ReturnObjectToPool(gameObject);
    }

    public void SetPool(Pool pool)
    {
        this.pool = pool;
    }
}
