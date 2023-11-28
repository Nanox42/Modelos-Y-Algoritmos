using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//nahuel stagno
public class BuilderParticles
{
    GameObject _particlePrefab;
    Vector3 _setPos;
    Vector3 _setScale;


    public BuilderParticles(GameObject p)
    {
        _particlePrefab = p;
    }

    public BuilderParticles SetPos(Vector3 pos)
    {
        _setPos = pos;
        return this;
    }

    public BuilderParticles SetScale(Vector3 scale)
    {
        _setScale = scale;
        return this;
    }

    public GameObject Done()
    {
        GameObject particleCreated = GameObject.Instantiate(_particlePrefab);

        particleCreated.transform.position = _setPos;
        particleCreated.transform.localScale = _setScale;

        return particleCreated;
    }
}
