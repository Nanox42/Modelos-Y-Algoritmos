using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabFactory : ObjectFactoryBase
{
    
    //private GameObject _prefab;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField] private Transform parent;
    public override GameObject CreateObject()
    {
        return Instantiate(_prefab, Vector3.zero, Quaternion.identity, parent);
    }

    public PrefabFactory(GameObject prefab)
    {
         _prefab = prefab;
    }

        //public GameObject CreatePrefab()
        //{
           //return Object.Instantiate(_prefab, default, Quaternion.identity);
          
        //}
    

}
