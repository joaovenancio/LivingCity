using UnityEngine;

public class ScreenRaycast : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Camera MainCamera;

	private IRaycastable _raycastableBeingDragged = null;
	private PointerData _pointer;

	public PointerData Pointer { get => _pointer; }



	private void Awake()
	{
		SetupFields();
	}
	
	private void SetupFields()
	{
		_pointer = new PointerData();
		if (!MainCamera) MainCamera = Camera.main;
	}


	public void OnPointerTap(Vector2 pointerPosition)
	{
		Vector3 worldPoint = Vector3.zero;
		IRaycastable raycastable = Raycast(pointerPosition, ref worldPoint);

		if (raycastable == null) return; //Event!!!
		else raycastable.OnTap(worldPoint);
	}


	public void OnPointerPressAndDrag(PointerDragState dragState, Vector2 pointerPosition, float timeOfEventTrigger)
	{
		Vector3 worldPoint = Vector3.zero;

		switch (dragState)
		{
			case PointerDragState.Started:
				IRaycastable raycastable = Raycast(pointerPosition, ref worldPoint);

				if (raycastable == null) return;
				if (!raycastable.IsDraggable) return;

				_raycastableBeingDragged = raycastable;

				raycastable.PointerData = _pointer;
				raycastable.OnDrag(worldPoint);

				break;
			case PointerDragState.Ended:

				if (_raycastableBeingDragged == null) return;

				_raycastableBeingDragged.PointerData = null;
				_raycastableBeingDragged.OnStopDrag();
				_raycastableBeingDragged = null;

				break;
		}
	}


	public void OnPointerPositionChange(Vector2 pointerPosition)
	{
		_pointer.WorldPosition = MainCamera.ScreenToWorldPoint(pointerPosition);
	}

	#region Auxiliary Methods
	public IRaycastable Raycast(Vector2 pointerPosition, ref Vector3 worldPoint)
	{
		worldPoint = MainCamera.ScreenToWorldPoint(pointerPosition);

		RaycastHit hit;

		if (!Physics.Raycast(worldPoint,
			Vector3.zero,
			out hit,
			Mathf.Infinity))
		{
			return null; //Event;
		}
			

		IRaycastable raycastable = hit.collider.GetComponent<IRaycastable>();

		if (raycastable == null) return null;
		else return raycastable;
	}
	#endregion
}
