using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
/// Manages player inputs.
/// </summary>
public class InputManager : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private PlayerInput _playerInput;

	[Header("Settings")]
	[SerializeField] private bool _logDebug = true;

	private InputAction _pointerPressAndDragAction;
	private InputAction _pointerTapAction;
	private InputAction _pointerPositionAction;
	private Vector2 _pointerPosition = Vector2.zero;



	[Header("Public Events")]
	[SerializeField] private UnityEvent<Vector2> _onPointerTap;
	public UnityEvent<Vector2> OnPointerTap { get => _onPointerTap; }
	[Space]
	[SerializeField] private UnityEvent<PointerDragState, Vector2, float> _onPointerPressAndDrag;
	public UnityEvent<PointerDragState, Vector2, float> OnPointerPressAndDrag { get => _onPointerPressAndDrag; }
	[Space]
	[SerializeField] private UnityEvent<Vector2> _onPointerPositionChange;
	public UnityEvent<Vector2> OnPointerPositionChange { get => _onPointerPositionChange; }



	private void Awake()
	{
		SetupInputActions();
		SetupEvents();
	}

	private void SetupEvents()
	{
		if (_onPointerTap == null) _onPointerTap = new UnityEvent<Vector2>();
		if (_onPointerPressAndDrag == null) _onPointerPressAndDrag = new UnityEvent<PointerDragState, Vector2, float>();
		if (_onPointerPositionChange == null) _onPointerPositionChange = new UnityEvent<Vector2>();
	}

	private void OnEnable()
	{
		EnableEvents();
	}

	private void OnDisable()
	{
		DisableEvents();
	}



	/// <summary>
	/// Sets up input actions.
	/// </summary>
	private void SetupInputActions()
	{
		_pointerPressAndDragAction = _playerInput.actions["PointerPressAndDrag"];
		_pointerTapAction = _playerInput.actions["PointerTap"];
		_pointerPositionAction = _playerInput.actions["PointerPosition"];
	}

	private void EnableEvents()
	{
		_pointerPressAndDragAction.started += OnPointerPressAndDragStarted;
		_pointerPressAndDragAction.canceled += OnPointerPressAndDragCanceled;

		_pointerTapAction.performed += OnPointerTapPerformed;

		_pointerPositionAction.performed += OnPointerPositionPerformed;
	}

	
	private void DisableEvents()
	{
		_pointerPressAndDragAction.started -= OnPointerPressAndDragStarted;
		_pointerPressAndDragAction.canceled -= OnPointerPressAndDragCanceled;

		_pointerTapAction.performed -= OnPointerTapPerformed;

		_pointerPositionAction.performed -= OnPointerPositionPerformed;
	}

	/// <summary>
	/// Called when the pointer position changes.
	/// </summary>
	/// <param name="context">The input action context.</param>
	private void OnPointerPositionPerformed(InputAction.CallbackContext context)
	{
		_pointerPosition = context.ReadValue<Vector2>();

		_onPointerPositionChange.Invoke(_pointerPosition);

		#if UNITY_EDITOR
		if (_logDebug) Debug.Log("Input Manager -> Pointer Position: " + _pointerPosition.ToString(), this);
		#endif
	}

	#region PointerPressAndDragEvents
	/// <summary>
	/// Called when the pointer press and drag action starts.
	/// </summary>
	/// <param name="context">The input action context.</param>
	private void OnPointerPressAndDragStarted(InputAction.CallbackContext context)
	{
		Vector2 pointerStartingLocation = context.ReadValue<Vector2>();

		_onPointerPressAndDrag.Invoke(
			PointerDragState.Started,
			pointerStartingLocation,
			Time.time);

		#if UNITY_EDITOR
		if (_logDebug) Debug.Log("Input Manager -> Pointer Press and Drag -> Started at: " + pointerStartingLocation, this);
		#endif
	}

	/// <summary>
	/// Called when the pointer press and drag action is canceled.
	/// </summary>
	/// <param name="context">The input action context.</param>
	private void OnPointerPressAndDragCanceled(InputAction.CallbackContext context)
	{
		_onPointerPressAndDrag.Invoke(
			PointerDragState.Ended,
			_pointerPosition,
			Time.time);

		#if UNITY_EDITOR
		if (_logDebug) Debug.Log("Input Manager -> Pointer Press and Drag -> Ended at: " + _pointerPosition, this);
		#endif
	}
	#endregion

	/// <summary>
	/// Called when the pointer tap action is performed.
	/// </summary>
	private void OnPointerTapPerformed(InputAction.CallbackContext context)
	{
		_onPointerTap.Invoke(_pointerPosition);

		#if UNITY_EDITOR
		if (_logDebug) Debug.Log("Input Manager -> Pointer Tap -> Performed at: " + _pointerPosition, this);
		#endif
	}
}

public enum PointerDragState
{
	Started,
	Ended
}
