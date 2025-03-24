//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Optional Reference")]
	[Tooltip("If not manually set, is needed to manually assign this class InputEvents functions to a InputEvents GameObject.")]
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
		_inputEvents?.OnPointerSingleTap.AddListener(OnPointerSingleTap);
	}

	private void OnDisable()
	{
		_inputEvents?.OnPointerDragStart.RemoveListener(OnPointerDragStart);
		_inputEvents?.OnPointerDragEnd.RemoveListener(OnPointerDragEnd);
		_inputEvents?.OnPointerSingleTap.RemoveListener(OnPointerSingleTap);
	}

	private void Awake()
	{
		SetupFields();
	}



	private void SetupFields()
	{
		if (!_inputEvents) _inputEvents = EventsManager.Instance.InputEvents;

		if (_onClick == null) _onClick = new UnityEvent();
		if (_onDragStart == null) _onDragStart = new UnityEvent();
		if (_onDragEnd == null) _onDragEnd = new UnityEvent();
	}

	#region InputEvents
	private void OnPointerDragStart(Vector2 screenPosition, float timestamp)
	{
		if (!IsInteractable) return;
		if (!_isPointerOver) return;
		//Debug.Log("ButtonUI: OnPointerDragStart");

		_onDragStart.Invoke();
	}

	private void OnPointerDragEnd(float timestamp)
	{
		if (!IsInteractable) return;
		if (!_isPointerOver) return;
		//Debug.Log("ButtonUI: OnPointerDragEnd");

		_onDragEnd.Invoke();
	}

	private void OnPointerSingleTap(Vector2 screenPosition)
	{
		if (!IsInteractable) return;
		if (!_isPointerOver) return;
		_onClick.Invoke();
	}

	#endregion

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
