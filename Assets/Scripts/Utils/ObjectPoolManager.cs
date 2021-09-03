using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ObjectPoolManager : MonoBehaviour
{
    //Class for handling Object Pools, two methods: initialization and retrieving from pool

    public static List<GameObject> InitializePool(GameObject poolContent, int initialSize)
    {
        List<GameObject> newPool = new List<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            GameObject tmp = Instantiate(poolContent);
            tmp.SetActive(false);
            newPool.Add(tmp);
        }
        return newPool;
    }

    //Retrieving from pool returnes existing object, or, if all objects are occupied - creates and adds
    //new one to the pool, returning it.
    public static GameObject RetrieveFromPool(List<GameObject> pool)  
    {
        int i = 0;

        while (i < pool.Count)
        {
            if (!pool[i].activeSelf) return pool[i];
            else i++;
        }

        GameObject tmp = Instantiate(pool[0]);

        pool.Add(tmp);

        return pool[i];
    }
}
