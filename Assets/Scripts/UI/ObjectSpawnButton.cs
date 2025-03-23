//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

public class ObjectSpawnButton : MonoBehaviour
{
    [Header("References")]
	[SerializeField] private RectFollowPosition _rectFollowPosition;
    [SerializeField] private InputEvents _inputEvents;
	[SerializeField] private PreviewObject _previewObject;
	[SerializeField] private ScreenRaycast _screenRaycaster;

	private bool _isBeingDragged = false;



	private void Awake()
	{
        SetupFields();
	}

    void Update()
    {
        UpdateImagePosition();
	}


	private void SetupFields()
	{
		if (!_rectFollowPosition) _rectFollowPosition = GetComponent<RectFollowPosition>();
		if (!_rectFollowPosition) Debug.LogError("ObjectSpawnButton: RectFollowPosition component is missing.", this);
		if (!_inputEvents) _inputEvents = EventsManager.Instance.InputEvents;
		if (!_inputEvents) Debug.LogError("ObjectSpawnButton: InputEvents component is missing.", this);
	}

	private void UpdateImagePosition()
    {
        if (!_isBeingDragged) return;

		_rectFollowPosition.PositionToFollow = _inputEvents.PointerPosition;

		Vector3 worldPoint = Vector3.zero;
		RaycastResult rayResult = _screenRaycaster.Raycast(_inputEvents.PointerPosition, ref worldPoint);

		if (rayResult.HitType.Equals(HitType.Nothing)) return;
		if (!rayResult.Collider.tag.Equals("Ground")) return;

		_previewObject.LocationToShow = worldPoint;


	}

	#region Button events
	public void OnDrag()
    {
		_isBeingDragged = true;
		_rectFollowPosition.IsFollowing = true;
		_previewObject.IsPreviewing = true;
	}

    public void OnDragEnd()
	{
		_isBeingDragged = false;
        _rectFollowPosition.IsFollowing = false;
		_rectFollowPosition.ResetPosition();
		_previewObject.IsPreviewing = false;
	}
	#endregion
}
