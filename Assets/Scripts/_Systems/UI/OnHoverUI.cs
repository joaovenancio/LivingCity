//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Manages UI hover events, triggering enter and exit events when the pointer interacts with the UI element.
/// </summary>
public class OnHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Optional Reference")]
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private InputEvents _inputEvents;
	[Header("Events")]
	[SerializeField] private UnityEvent<RectTransform> _onEnter;
	[SerializeField] private UnityEvent<RectTransform> _onExit;
	private bool _isUIBeingDragged = false;
	public bool _isPointerOver = false;

	public UnityEvent<RectTransform> OnEnter { get => _onEnter; }
	public UnityEvent<RectTransform> OnExit { get => _onExit; }



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



	private void SetupFields()
	{
		if (!_rectTransform) _rectTransform = this.GetComponent<RectTransform>();
		if (!_rectTransform) _rectTransform = this.GetComponentInChildren<RectTransform>();
		if (!_rectTransform) Debug.LogWarning("No RectTransform found in " + this.gameObject.name + " or its children.");

		if (!_inputEvents) _inputEvents = EventsManager.Instance.InputEvents;
	}

	/// <summary>
	/// Triggers the OnExit event and invokes the OnPointerHoverUI event.
	/// </summary>
	private void ExitUI()
	{
		OnExit.Invoke(_rectTransform);
		_inputEvents?.OnPointerHoverUI.Invoke(_rectTransform, HoverMotionType.Exit);
	}

	#region Input Events
	/// <summary>
	/// Handles the start of a pointer drag event.
	/// </summary>
	/// <remarks>
	/// Sets the _isUIBeingDragged flag to true if the pointer is over the UI element.
	/// </remarks>
	private void OnPointerDragStart(Vector2 screenPosition, float timestamp)
	{
		if (_isPointerOver) _isUIBeingDragged = true;
	}

	/// <summary>
	/// Handles the end of a pointer drag event.
	/// </summary>
	/// <remarks>
	/// Resets the _isUIBeingDragged flag and triggers the OnExit event if the pointer is not over the UI element.
	/// </remarks>
	private void OnPointerDragEnd(float timestamp)
	{
		_isUIBeingDragged = false;

		if (_isPointerOver) return;

		ExitUI();
	}
	#endregion

	#region Interfaces
	/// <summary>
	/// Handles the pointer entering the UI element.
	/// </summary>
	/// <remarks>
	/// Sets the _isPointerOver flag to true and triggers the OnEnter event if the UI is not being dragged.
	/// </remarks>
	public void OnPointerEnter(PointerEventData eventData)
	{
		_isPointerOver = true;

		if (_isUIBeingDragged) return;

		_inputEvents?.OnPointerHoverUI.Invoke(_rectTransform, HoverMotionType.Enter);
		OnEnter.Invoke(_rectTransform);
	}

	/// <summary>
	/// Handles the pointer exiting the UI element.
	/// </summary>
	/// <remarks>
	/// Sets the _isPointerOver flag to false and triggers the OnExit event if the UI is not being dragged.
	/// </remarks>
	public void OnPointerExit(PointerEventData eventData)
	{
		_isPointerOver = false;

		if (_isUIBeingDragged) return;

		ExitUI();
	}
	#endregion
}
