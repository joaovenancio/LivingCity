//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAnimationOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Animator _animator;

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Pointer entered");
		_animator.SetTrigger("SwipeUp");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Pointer exited");

		_animator.SetTrigger("SwipeDown");
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
