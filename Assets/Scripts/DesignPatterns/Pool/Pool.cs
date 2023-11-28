using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool : MonoBehaviour
{
    [SerializeField] private PrefabFactory _factory;
    private Stack<GameObject> poolStack = new Stack<GameObject>();
    [SerializeField] private int initialSize = 10;

    private void Start()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = _factory.CreateObject();
            obj.SetActive(false);
            poolStack.Push(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (poolStack.Count > 0)
        {
            GameObject obj = poolStack.Pop();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObject = _factory.CreateObject();
            return newObject;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolStack.Push(obj);
    }
}
