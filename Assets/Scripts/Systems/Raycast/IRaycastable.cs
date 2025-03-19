//Copyright(C) 2025 Jo�o V�tor Demaria Ven�ncio under GNU AGPL. Refer to README.md for more informations.
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
