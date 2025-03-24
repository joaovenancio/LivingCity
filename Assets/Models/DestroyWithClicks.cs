//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

public class DestroyWithClicks : MonoBehaviour, IRaycastable
{

	public int HitPoints = 3;
	public Rigidbody _rigidBody;
	public float _impulseForce = 8;
	public AudioSource _hitAudioSource;
	public AudioSource _explosionAudioSource;

	private bool _isDraggable = false;
	private PointerData _pointerData;
	public PointerData PointerData { get => _pointerData; set => _pointerData = value; }


	public bool IsDraggable => _isDraggable;

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void OnTap(Vector2 worldPoint)
	{
		HitPoints--;
		_hitAudioSource?.Play();
		if (HitPoints <= 0)
		{
			_rigidBody.useGravity = true;
			_rigidBody.isKinematic = false;
			_rigidBody.AddForce(Camera.main.transform.up * _impulseForce, ForceMode.Impulse);
			_explosionAudioSource?.Play();
		}
	}

	public void OnDrag(Vector2 worldPoint)
	{
	}

	public void OnStopDrag()
	{
	}
}
