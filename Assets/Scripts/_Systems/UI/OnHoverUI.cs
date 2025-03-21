//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Optional Reference")]
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private InputEvents _inputEvents;
	[Header("Events")]
	[SerializeField] private UnityEvent<RectTransform> _onEnter;
	[SerializeField] private UnityEvent<RectTransform> _onExit;

	public UnityEvent<RectTransform> OnEnter { get => _onEnter; }
	public UnityEvent<RectTransform> OnExit { get => _onExit; }



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



	#region Interfaces
	public void OnPointerEnter(PointerEventData eventData)
	{
		OnEnter.Invoke(_rectTransform);
		_inputEvents?.OnPointerHoverUI.Invoke(_rectTransform, HoverMotionType.Enter);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnExit.Invoke(_rectTransform);
		_inputEvents?.OnPointerHoverUI.Invoke(_rectTransform, HoverMotionType.Exit);
	}
	#endregion
}
