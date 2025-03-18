using UnityEngine;

public class PointerData
{
	public Vector2 WorldPosition;

	public PointerData()
	{
		WorldPosition = Vector2.zero;
	}

	public PointerData(Vector2 worldPosition)
	{
		WorldPosition = worldPosition;
	}
}
