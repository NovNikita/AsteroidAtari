using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{

    private List<GameObject> poolList;
    private Transform parentObject;


    public ObjectPool(GameObject poolContent, int poolSize, Transform poolParentObject)
    {
        poolList = new List<GameObject>();
        parentObject = poolParentObject;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject objectToAdd = GameObject.Instantiate(poolContent, parentObject);
            objectToAdd.SetActive(false);
            poolList.Add(objectToAdd);
        }
    }



    public GameObject RetrieveFromPool()
    {
        int i = 0;

        while (i < poolList.Count)
        {
            if (!poolList[i].activeSelf) return poolList[i];
            else i++;
        }

        GameObject tmp = GameObject.Instantiate(poolList[0], parentObject);

        poolList.Add(tmp);

        return poolList[i];
    }

    public void ResetPool()
    {
        for (int i = 0; i < poolList.Count; i++)
            poolList[i].gameObject.SetActive(false);
    }
}
