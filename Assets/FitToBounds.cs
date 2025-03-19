//Copyright(C) 2025 João Vítor Demaria Venâncio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

public class FitToBounds : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private MeshRenderer _targetMeshToResize;
	[SerializeField] private MeshRenderer _reference;



	void Start()
	{
		FitWidthLenght();
	}



	/// <summary>
	/// Adjusts the width and length of the target mesh to match the reference mesh.
	/// </summary>
	/// <returns>True if the target mesh and reference mesh are not null and the scaling was applied; otherwise, false.</returns>
	public bool FitWidthLenght()
	{
		if (_targetMeshToResize == null || _reference == null) return false;

		Bounds targetBounds = _targetMeshToResize.bounds;
		Bounds referenceBounds = _reference.bounds;

		Vector3 scale = _targetMeshToResize.transform.localScale;
		scale.x *= referenceBounds.size.x / targetBounds.size.x;
		scale.z *= referenceBounds.size.z / targetBounds.size.z;

		_targetMeshToResize.transform.localScale = scale;

		return true;
	}
}
