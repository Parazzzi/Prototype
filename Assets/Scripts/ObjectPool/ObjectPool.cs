using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> objectPool = new List<GameObject>();
    private int initialSize;
    private GameObject prefab;

    public ObjectPool(GameObject _prefab, int _initialSize)
    {
        prefab = _prefab;
        initialSize = _initialSize;
        InitializePool(prefab, initialSize);
    }

    public void InitializePool(GameObject prefab, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObject = Object.Instantiate(prefab);
        objectPool.Add(newObject);
        return newObject;
    }
}
