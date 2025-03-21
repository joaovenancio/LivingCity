//Copyright(C) 2025 João Vítor Demaria Venâncio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10)]
public class InputEvents : MonoBehaviour
{
	[Header("Public Events")]
	[Space]
	[SerializeField] private UnityEvent<Vector2, float> _onDragNothing;
	[Space]
	[SerializeField] private UnityEvent<float> _onPointerDragEnd;
	[Space]
	[SerializeField] private UnityEvent<Vector2> _onPointerMove;
	[Space]
	[SerializeField] private UnityEvent<Vector2> _onPointerDirectionChange;
	[Space]
	[SerializeField] private UnityEvent<RectTransform, HoverMotionType> _onPointerHoverUI;
	[Space]
	[SerializeField] private UnityEvent<Vector2, float> _onPointerDragStart;


	/// <summary>
	/// Event triggered when a drag action occurs without any specific target.
	/// </summary>
	/// <remarks>
	/// The Vector2 parameter represents the position of the pointer during the drag action.
	/// </remarks>
	public UnityEvent<Vector2, float> OnDragNothing { get => _onDragNothing; }
	/// <summary>
	/// Event triggered when the pointer drag action ends.
	/// </summary>
	public UnityEvent<float> OnPointerDragEnd { get => _onPointerDragEnd; }
	/// <summary>
	/// Event triggered when the pointer moves.
	/// </summary>
	/// <remarks>
	/// The Vector2 parameter represents the new position of the pointer.
	/// </remarks>
	public UnityEvent<Vector2> OnPointerMove { get => _onPointerMove; }

	public UnityEvent<Vector2> OnPointerDirectionChange { get => _onPointerDirectionChange; }

	public UnityEvent<RectTransform, HoverMotionType> OnPointerHoverUI { get => _onPointerHoverUI; }

	public UnityEvent<Vector2, float> OnPointerDragStart { get => _onPointerDragStart; }


	private void Awake()
	{
		SetupFields();
	}



	private void SetupFields()
	{
		if (_onDragNothing == null) _onDragNothing = new UnityEvent<Vector2, float>();
		if (_onPointerDragEnd == null) _onPointerDragEnd = new UnityEvent<float>();
		if (_onPointerMove == null) _onPointerMove = new UnityEvent<Vector2>();
		if (_onPointerDirectionChange == null) _onPointerDirectionChange = new UnityEvent<Vector2>();
	}

	public void UpdatePointerPositionChange(Vector2 pointerPosition)
	{
		OnPointerMove.Invoke(pointerPosition);
	}

	public void UpdatePointerDirectionChange(Vector2 pointerDirection)
	{
		OnPointerDirectionChange.Invoke(pointerDirection);
	}

	public void OnPointerPressAndDrag(PointerDragState dragState, Vector2 pointerStartingLocation, float timestamp)
	{
		switch(dragState)
		{
			case PointerDragState.Started:
				OnPointerDragStart.Invoke(pointerStartingLocation, timestamp);
				break;

			case PointerDragState.Ended:
				OnPointerDragEnd.Invoke(timestamp);
				break;
		}
	}
}

public enum HoverMotionType
{
	Enter,
	Exit
}