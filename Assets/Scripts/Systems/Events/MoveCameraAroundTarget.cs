//Copyright(C) 2025 João Vítor Demaria Venâncio under GNU AGPL. Refer to README.md for more information.
using System;
using UnityEngine;

public class MoveCameraAroundTarget : MonoBehaviour
{
	[Header("References")]
    [SerializeField] private Transform _target;
	[Header("Settings")]
	[SerializeField] private float _sensitivity = 10f;
	private bool _isPointerMoving = false;
	private Vector2 _pointerDirection = Vector2.zero;



	private void Update()
	{
		if (!_isPointerMoving) return;

		RotateTarget();
	}

	private void RotateTarget()
	{
		float horizontalMovement = _pointerDirection.x * _sensitivity * Time.deltaTime;
		float verticalMovement = _pointerDirection.y * _sensitivity * Time.deltaTime;

		_target.rotation = Quaternion.Euler(_target.eulerAngles.x + verticalMovement, _target.eulerAngles.y + horizontalMovement, 0);

		//_target.Rotate(verticalMovement, horizontalMovement, 0.0f, Space.World);
	}

	public void ChangePointerDirection(Vector2 pointerPosition)
	{
		_pointerDirection = pointerPosition;
	}

	#region Events
	public void OnDragNothing(Vector2 pointerPosition, float timestamp)
    {
        EventsManager.Instance.InputEvents.OnPointerDirectionChange.AddListener(ChangePointerDirection);
		_isPointerMoving = true;
	}

    public void OnPointerDragEnd(float timestamp)
	{
		EventsManager.Instance.InputEvents.OnPointerDirectionChange.RemoveListener(ChangePointerDirection);
		_isPointerMoving = false;
	}
	#endregion


}
