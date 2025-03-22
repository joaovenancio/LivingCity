//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

/// <summary>
/// This class allows a RectTransform to follow a specified position in the UI.
/// </summary>
public class RectFollowPosition : MonoBehaviour
{
	[Header("Reference")]
	[SerializeField] private RectTransform _target;
	private Vector2 _originPosition = Vector2.zero;
	[Header("Variables")]
	/// <summary>
	/// The position that the RectTransform will follow.
	/// </summary>
	public Vector2 PositionToFollow = Vector2.zero;
	/// <summary>
	/// Determines whether the RectTransform is currently following the position.
	/// </summary>
	public bool IsFollowing = false;
	/// <summary>
	/// The speed at which the RectTransform will move towards the target position.
	/// </summary>
	public float Speed = 1f;
	/// <summary>
	/// The RectTransform that will move.
	/// </summary>
	public RectTransform Target { 
		get => _target;
		set
		{
			_target = value;
			_originPosition = _target.localPosition;
		} 
	}



	private void Awake()
	{
		SetupFields();
	}

    void Update()
    {
        FollowPosition();
	}



    private void SetupFields()
    {
		if (!_target) _target = GetComponent<RectTransform>();
		if (!_target)
		{
			IsFollowing = false;
			Debug.LogError("RectFollowPosition: No RectTransform found.", this);
		}
		else _originPosition = _target.localPosition;

	}

	/// <summary>
	/// Moves the RectTransform towards the specified position if following is enabled.
	/// </summary>
	private void FollowPosition()
	{
		if (!IsFollowing) return;

		Target.position = Vector3.MoveTowards(Target.position, PositionToFollow, Speed);
	}

	/// <summary>
	/// Resets the RectTransform's position to its original position.
	/// </summary>
	public void ResetPosition()
	{
		_target.localPosition = _originPosition;
	}
}
