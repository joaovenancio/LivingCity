//Copyright(C) 2025 João Vítor Demaria Venâncio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

public class ScreenRaycast : Singleton<ScreenRaycast>
{
	[Header("References")]
	[SerializeField] private Camera MainCamera;

	private IRaycastable _raycastableBeingDragged = null;
	private PointerData _pointer;

	[Header("Variables")]
	public bool IsEmittingEvents = true;

	public PointerData Pointer { get => _pointer; }



	private void Awake()
	{
		SetupSingleton();
		SetupFields();
	}
	
	private void SetupFields()
	{
		_pointer = new PointerData();
		if (!MainCamera) MainCamera = Camera.main;
	}


	public void OnPointerTap(Vector2 pointerPosition)
	{
		if (!IsEmittingEvents) return;

		Vector3 worldPoint = Vector3.zero;
		RaycastResult raycastResult = Raycast(pointerPosition, ref worldPoint);

		if (raycastResult.HitType == HitType.IRaycastable) raycastResult.Raycastable.OnTap(worldPoint);
	}


	public void OnPointerPressAndDrag(PointerDragState dragState, Vector2 pointerPosition, float timeOfEventTrigger)
	{
		if (!IsEmittingEvents) return;

		Vector3 worldPoint = Vector3.zero;

		switch (dragState)
		{
			case PointerDragState.Started:
				RaycastResult raycastResult = Raycast(pointerPosition, ref worldPoint);

				if (raycastResult.HitType == HitType.Nothing)
				{
					EventsManager.Instance.InputEvents.OnDragNothing.Invoke(worldPoint, timeOfEventTrigger);
					return;
				}

				if (raycastResult.HitType == HitType.Something) return;

				IRaycastable raycastable = raycastResult.Raycastable;

				if (!raycastable.IsDraggable) return;

				_raycastableBeingDragged = raycastable;

				raycastable.PointerData = _pointer;
				raycastable.OnDrag(worldPoint);

				break;
			case PointerDragState.Ended:

				EndPointerDrag();

				break;
		}
	}

	private void EndPointerDrag()
	{
		//EventsManager.Instance.InputEvents.OnPointerDragEnd.Invoke(timeOfEventTrigger);

		if (_raycastableBeingDragged == null) return;

		_raycastableBeingDragged.PointerData = null;
		_raycastableBeingDragged.OnStopDrag();
		_raycastableBeingDragged = null;
	}


	public void OnPointerHoverUI(RectTransform sender, HoverMotionType type)
	{
		switch (type)
		{
			case HoverMotionType.Enter:
				IsEmittingEvents = false;
				EventsManager.Instance?.InputEvents?.OnPointerDragEnd?.Invoke(0f);
				EndPointerDrag();
				break;

			case HoverMotionType.Exit:
				IsEmittingEvents = true;
				break;
		}
	}


	public void OnPointerPositionChange(Vector2 pointerPosition)
	{
		_pointer.WorldPosition = MainCamera.ScreenToWorldPoint(pointerPosition);
	}

	#region Auxiliary Methods
	public RaycastResult Raycast(Vector2 pointerPosition, ref Vector3 worldPoint)
	{
		worldPoint = MainCamera.ScreenToWorldPoint(pointerPosition);

		RaycastHit hit;

		if (!Physics.Raycast(worldPoint,
			Vector3.zero,
			out hit,
			Mathf.Infinity)) return new RaycastResult(HitType.Nothing, null);
			//EventsManager.Instance.InputEvents.OnDragNothing.Invoke(worldPoint);
			

		IRaycastable raycastable = hit.collider.GetComponent<IRaycastable>();

		if (raycastable == null) return new RaycastResult(HitType.Something, null);
		else return new RaycastResult(HitType.IRaycastable, raycastable);
	}
	#endregion
}

public struct RaycastResult
{
	public IRaycastable Raycastable;
	public HitType HitType;

	public RaycastResult(HitType hitType, IRaycastable raycastable)
	{
		HitType = hitType;
		Raycastable = raycastable;
	}
}

public enum HitType
{
	Nothing,
	Something,
	IRaycastable,
}
