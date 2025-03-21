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
	[SerializeField] private UnityEvent _onClick;
	[SerializeField] private UnityEvent _onDragStart;
	[SerializeField] private UnityEvent _onDragEnd;
	private bool _isPointerOver = false;

	public UnityEvent OnClick { get => _onClick; }
	public UnityEvent OnDragStart { get => _onDragStart; }
	public UnityEvent OnDragEnd { get => _onDragEnd; }



	private void Awake()
	{
		SetupFields();
		SetupInputEvents();
	}



	private void SetupInputEvents()
	{
		
	}

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
