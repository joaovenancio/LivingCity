//Copyright(C) 2025 João Vítor Demaria Venâncio under GNU AGPL. Refer to README.md for more informations.
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10)]
public class EventsManager : Singleton<EventsManager>
{

	[SerializeField] private InputEvents _inputEvents;

	public InputEvents InputEvents { get => _inputEvents; }



	private void Awake()
	{
		SingletonSetup();
		SetupFields();
	}

	private void SetupFields()
	{
		if (_inputEvents == null)
		{
			GameObject inputEventsGameObject = new GameObject("InputEvents");
			inputEventsGameObject.transform.SetParent(this.transform);
			inputEventsGameObject.AddComponent<InputEvents>();
		}
	}
}
