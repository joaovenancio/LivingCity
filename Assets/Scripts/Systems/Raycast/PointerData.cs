//Copyright(C) 2025 Jo�o V�tor Demaria Ven�ncio under GNU AGPL. Refer to README.md for more informations.
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
