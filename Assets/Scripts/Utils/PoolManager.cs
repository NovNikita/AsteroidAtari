using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
	private static PoolPart[] pools;
	private static GameObject objectsParent;

	[System.Serializable]
	public struct PoolPart
	{
		public GameObject prefab;	//prefab for pool
		public int count;			//initial size of a pool
		public ObjectPool pool;		//reference to pool itself
	}


	public static void Initialize(PoolPart[] newPools)
	{
		//Create an empty object to be parent for all pool objects
		objectsParent = new GameObject();
		objectsParent.name = "ObjectsFromPools";

		pools = newPools;

		//create pool for each entered prefab(setup in Unity Editor)
		for (int i = 0; i < pools.Length; i++)
		{
			if (pools[i].prefab != null)
			{
				pools[i].pool = new ObjectPool(pools[i].prefab, pools[i].count, objectsParent.transform);				
			}
		}
	}


	//method to retrieve object from pull by prefab reference
	public static GameObject GetObject(GameObject objectToGet)
	{
		GameObject result = null;

		if (pools != null)
		{
			for (int i = 0; i < pools.Length; i++)
			{

				if (pools[i].prefab == objectToGet)
				{ 
					result = pools[i].pool.RetrieveFromPool(); 
					return result;
				}

			}
		}

		return result; //return null if there is no pool for requested prefab
	}

	public static void ResetAllPools()
    {
		for (int i = 0; i < pools.Length; i++)
			pools[i].pool.ResetPool();	
    }
}
