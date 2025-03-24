//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

public class GetHit : MonoBehaviour, IRaycastable
{
	public int HitPoints = 3;
	[SerializeField] private Animator _animator;
	public Rigidbody _rigidBody;
	public float _impulseForce = 10;
	public AudioSource _hitAudioSource;
	public AudioSource _impactAudioSource;

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
		Debug.Log("Getting fidged");
		HitPoints--;
		_animator.SetTrigger("Hit");
		_hitAudioSource?.Play();
		if (HitPoints <= 0)
		{
			_rigidBody.useGravity = true;
			_rigidBody.isKinematic = false;
			_rigidBody.AddForce(Camera.main.transform.forward * _impulseForce, ForceMode.Impulse);
			_impactAudioSource?.Play();
		}
	}

	public void OnDrag(Vector2 worldPoint)
	{
		throw new System.NotImplementedException();
	}

	public void OnStopDrag()
	{
		throw new System.NotImplementedException();
	}
}
