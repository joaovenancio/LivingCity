using UnityEngine;

public interface IRaycastable
{
	public PointerData PointerData { get; set; }
	public bool IsDraggable { get; }

	//public void OnRaycast(Vector2 worldPoint);
	public void OnTap(Vector2 worldPoint);
	public void OnDrag(Vector2 worldPoint);
	public void OnStopDrag();
}
