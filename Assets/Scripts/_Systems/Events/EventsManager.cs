//Copyright(C) 2025 João Vítor Demaria Venâncio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10)]
public class EventsManager : Singleton<EventsManager>
{
	[Header("Optional Reference")]
	[SerializeField] private InputEvents _inputEvents;
	[SerializeField] private SpawnEvents _spawnEvents;

	public InputEvents InputEvents { get => _inputEvents; }
	public SpawnEvents SpawnEvents { get => _spawnEvents; }



	private void Awake()
	{
		SetupSingleton();
		SetupFields();
	}



	private void SetupFields()
	{
		ConfigureInputEvents();
		ConfigureSpawnEvents();
	}

	private void ConfigureInputEvents()
	{
		if (_inputEvents) return;

		_inputEvents = this.GetComponent<InputEvents>();

		if (_inputEvents) return;

		_inputEvents = this.GetComponentInChildren<InputEvents>();

		if (_inputEvents) return;

		GameObject inputEventsGameObject = new GameObject("Input Events");
		inputEventsGameObject.transform.SetParent(this.transform);
		_inputEvents = inputEventsGameObject.AddComponent<InputEvents>();
	}

	private void ConfigureSpawnEvents()
	{
		if (_spawnEvents) return;

		_spawnEvents = this.GetComponent<SpawnEvents>();

		if (_spawnEvents) return;

		_spawnEvents = this.GetComponentInChildren<SpawnEvents>();

		if (_spawnEvents) return;

		GameObject spawnEventsGameObject = new GameObject("Spawn Events");
		spawnEventsGameObject.transform.SetParent(this.transform);
		_spawnEvents = spawnEventsGameObject.AddComponent<SpawnEvents>();
	}
}
