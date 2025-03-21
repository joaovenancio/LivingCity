//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Optional Reference")]
	[Tooltip("If not manually set, is needed to manually assign this class InputEvents functions to a GameObject with a InputEvents")]
	[SerializeField] private InputEvents _inputEvents;
	[Header("Events")]
	[Space]
	[SerializeField] private UnityEvent _onClick;
	[Space]
	[SerializeField] private UnityEvent _onDragStart;
	[Space]
	[SerializeField] private UnityEvent _onDragEnd;
	private bool _isPointerOver = false;


	public bool IsInteractable = true;
	/// <summary>
	/// Event triggered when the button is clicked.
	/// </summary>
	public UnityEvent OnClick { get => _onClick; }
	/// <summary>
	/// Event triggered when dragging starts on the button.
	/// </summary>
	public UnityEvent OnDragStart { get => _onDragStart; }
	/// <summary>
	/// Event triggered when dragging ends on the button.
	/// </summary>
	public UnityEvent OnDragEnd { get => _onDragEnd; }


	private void OnEnable()
	{
		_inputEvents?.OnPointerDragStart.AddListener(OnPointerDragStart);
		_inputEvents?.OnPointerDragEnd.AddListener(OnPointerDragEnd);
	}

	private void OnDisable()
	{
		_inputEvents?.OnPointerDragStart.RemoveListener(OnPointerDragStart);
		_inputEvents?.OnPointerDragEnd.RemoveListener(OnPointerDragEnd);
	}


	private void Awake()
	{
		SetupFields();
	}

	#region InputEvents
	private void OnPointerDragStart(Vector2 screenPosition, float timestamp)
	{
		if (!IsInteractable) return;
		if (!_isPointerOver) return;

		_onDragStart.Invoke();
	}

	private void OnPointerDragEnd(float timestamp)
	{
		if (!IsInteractable) return;
		if (!_isPointerOver) return;

		_onDragEnd.Invoke();
	}
	#endregion



	/// <summary>
	/// Sets up the fields for the ButtonUI component.
	/// </summary>
	private void SetupFields()
	{
		if (!_inputEvents) _inputEvents = EventsManager.Instance.InputEvents;

		if (_onClick == null) _onClick = new UnityEvent();
		if (_onDragStart == null) _onDragStart = new UnityEvent();
		if (_onDragEnd == null) _onDragEnd = new UnityEvent();
	}

	#region Interfaces
	public void OnPointerEnter(PointerEventData eventData)
	{
		_isPointerOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_isPointerOver = false;
	}
	#endregion
}
