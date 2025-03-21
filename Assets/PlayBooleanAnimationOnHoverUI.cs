//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayBooleanAnimationOnHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Reference")]
	public Animator _animator;
	[Header("Settings")]
	public string _parameterName;



	public void OnPointerEnter(PointerEventData eventData)
	{
		PlayEnter();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		PlayExit();
	}



	public void PlayEnter()
	{
		_animator.SetBool(_parameterName, true);
	}

	public void PlayExit()
	{
		_animator.SetBool(_parameterName, false);
	}
}
