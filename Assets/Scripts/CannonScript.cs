using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonScript : MonoBehaviour   //no es factory
{
    [SerializeField] private Pool poolBasic;
    //[SerializeField] private float duration=3.5f;
    [SerializeField] private float initialDelay=1f;
    [SerializeField] private float repeatRate=1f;
    [SerializeField] private Transform parentParticle;

    private void Start()
    {
        StartCoroutine(CannonCoroutine(initialDelay,repeatRate));
    }


    IEnumerator CannonCoroutine(float initialDelay, float repeatRate)
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(repeatRate);
        }
    }

    public void Fire()
    {
        GameObject obj = poolBasic.GetObjectFromPool();
        CannonBall cannonBall = obj.GetComponent<CannonBall>();
        if (cannonBall != null)
        {
            cannonBall.SetPool(poolBasic);
        }
        ParticleFxBuilder();
    }

    //nahuel stagno

    [SerializeField] GameObject _particlePrefab;
    [SerializeField] Transform _spawnPoint;

    public void ParticleFxBuilder() //Builder
    {
        GameObject particle = new BuilderParticles(_particlePrefab)
                              .SetPos(_spawnPoint.position)
                              .SetScale(Vector3.one / 2)
                              .Done();

        particle.transform.SetParent(parentParticle);
    }
}
