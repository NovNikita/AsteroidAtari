using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSetup : MonoBehaviour
{

	#region Unity scene settings

	//struct with prefabs and sizes for pools, filled in Unity Editor
	[SerializeField] private PoolManager.PoolPart[] pools; 
	
	#endregion


	void Awake()
	{
		Initialize();
	}

	void Initialize()
	{
		PoolManager.Initialize(pools); 
	}
}
