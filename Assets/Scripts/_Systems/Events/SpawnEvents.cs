//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;
using UnityEngine.Events;

public class SpawnEvents : MonoBehaviour
{
	[Header("Public Events")]
	[Space]
	[SerializeField] private UnityEvent<GameObject, Vector3, Transform> _onSpawnObject;

	public UnityEvent<GameObject, Vector3, Transform> OnSpawnObject { get => _onSpawnObject; }


	public void Awake()
	{
		SetupFields();
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }


	private void SetupFields()
	{
		if (_onSpawnObject == null) _onSpawnObject = new UnityEvent<GameObject, Vector3, Transform>();
	}
}
