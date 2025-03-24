//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour //TODO: Implement object pooling
{

	[SerializeField] private GameObject _objectContainer;

	private void OnEnable()
	{
		EventsManager.Instance.SpawnEvents.OnSpawnObject.AddListener(SpawnObject);
    }

	private void OnDisable()
	{
		EventsManager.Instance.SpawnEvents.OnSpawnObject.RemoveListener(SpawnObject);
	}

	

	void Start()
    {
        
    }

    void Update()
    {
        
    }



	private void SpawnObject(GameObject prefabToSpawn, Vector3 position, Transform parent)
	{
		if (!prefabToSpawn)
		{
			Debug.LogError("ObjectSpawner: Prefab to spawn is null.", this);
			return;
		}

		GameObject newGameObject = GameObject.Instantiate(prefabToSpawn, _objectContainer.transform);
		newGameObject.transform.position = position;
	}
}
