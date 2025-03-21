//Copyright(C) 2025 João Vítor Demaria Venâncio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10)]
public class EventsManager : Singleton<EventsManager>
{
	[Header("Optional Reference")]
	[SerializeField] private InputEvents _inputEvents;

	public InputEvents InputEvents { get => _inputEvents; }



	private void Awake()
	{
		SetupSingleton();
		SetupFields();
	}



	private void SetupFields()
	{
		ConfigureIntupEvents();
	}

	private void ConfigureIntupEvents()
	{
		if (_inputEvents) return;

		_inputEvents = this.GetComponent<InputEvents>();

		if (_inputEvents) return;

		_inputEvents = this.GetComponentInChildren<InputEvents>();

		if (_inputEvents) return;

		GameObject inputEventsGameObject = new GameObject("InputEvents");
		inputEventsGameObject.transform.SetParent(this.transform);
		_inputEvents = inputEventsGameObject.AddComponent<InputEvents>();
	}
}
