//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

public class TiltWorld : MonoBehaviour, IRaycastable
{
    [SerializeField] private Transform _targetToRotate;

	private bool _isDraggable = true;
	private PointerData _pointerData;
	public PointerData PointerData { get => _pointerData; set => _pointerData = value; }
	public bool IsDraggable => _isDraggable;
	public Vector2 _pointerDirection = Vector2.zero;
	public float _sensitivity = 10;

	[SerializeField] private bool _isDragging = false;

	public void OnDrag(Vector2 worldPoint)
	{
		_isDragging = true;
	}

	public void OnStopDrag()
	{
		_isDragging = false;
	}

	public void OnTap(Vector2 worldPoint)
	{
		
	}

	public void OnPointerDirectionChange(Vector2 direction)
	{
		_pointerDirection = direction;
	}

	void Start()
    {
        
    }

    void Update()
    {
        if (!_isDragging) return;

		float horizontalMovement = _pointerDirection.x * _sensitivity * Time.deltaTime;
		float verticalMovement = _pointerDirection.y * _sensitivity * Time.deltaTime;

		_targetToRotate.rotation = Quaternion.Euler(_targetToRotate.eulerAngles.x + verticalMovement, _targetToRotate.eulerAngles.y + horizontalMovement, 0);
	}


}
